using Dapper;
using DynamicsGPAddin1.DataAccess;
using DynamicsGPAddin1.Models;
using Microsoft.Dexterity.Applications;
using Microsoft.Dexterity.Applications.SsgDictionary;
using Microsoft.Dexterity.Bridge;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;



namespace DynamicsGPAddin1
{
    public static  class  SqlCrud 
    { 
            public static string ConnectionStringGP
            {

            get
            {
                Microsoft.Dexterity.Applications.DynamicsDictionary.SyBackupRestoreForm backup;
                backup = Dynamics.Forms.SyBackupRestore;
                string gpLoginType = "SQL";
                string gpServer = backup.Functions.GetServerNameWithInstance.Invoke();
                string gpDatabase = Dynamics.Globals.IntercompanyId.Value;
                string gpUser = Dynamics.Globals.UserId.Value;
                string gpPassword = Dynamics.Globals.SqlPassword.Value;

                if (gpLoginType == "SQL" && gpServer != string.Empty && gpDatabase != string.Empty && gpUser != string.Empty && gpPassword != string.Empty)
                {
                    //Return connection string for ".NET Framework Data Provider for SQL Server"  (System.Data.SqlClient.SqlConnection)
                    return @"Data Source=" + gpServer + ";Initial Catalog=" + gpDatabase + ";User ID=" + gpUser + ";Password=" + gpPassword + ";";
                }
                else if (gpLoginType.ToUpper() == "WINDOWS" && gpServer != string.Empty && gpDatabase != string.Empty)
                {
                    //Return connection string for ".NET Framework Data Provider for SQL Server"  (System.Data.SqlClient.SqlConnection)
                    return @"Data Source=" + gpServer + ";Initial Catalog=" + gpDatabase + ";Integrated Security=SSPI;";
                }
                else
                {
                    return "";
                }

            }

        }
        
        private static SqlConnection CreateGPConnection()
        {
            SqlConnection sqlConn = ZeptoConnNet.ZeptoGPConn.GetConnection(Dynamics.Globals.SqlDataSourceName.Value, Controller.Instance.Model.GPCompanyDB, Dynamics.Globals.UserId.Value, Dynamics.Globals.SqlPassword.Value);
            return sqlConn;
        }

        //private readonly string _connectionString;
        private static GetGP db = new GetGP();

        //public SqlCrud(string connectionString)
        //{
        //    _connectionString = connectionString;

        //}      
    
        public static SqlConnection ConnectionGP()
        {
            SqlConnection sqlConn = new SqlConnection();
            sqlConn = CreateGPConnection();

            return sqlConn;
        }

        public static List<BomModel> GetBomInfo(string trxId)

        {
            //var parameters = new DynamicParameters();
            //parameters.Add("@Trx_ID", trxId, DbType.String, ParameterDirection.Input);

            string sql = @"Select * from BM10400 B left outer join EXT_ROYALTY_INFO on B.ITEMNMBR = [Item Number] where TRX_ID = @Trx_ID ";

            return db.LoadData<BomModel, dynamic>(sql, new { Trx_ID = trxId }, ConnectionGP());

        }
        public static List<TrxModel> GetSopInfo(string trxId)
        {
            string sql = @"Select S.SOPTYPE, S.SOPNUMBE, L.LNITMSEQ, L.ITEMNMBR, L.SERLTNUM from SOP30300 S INNER JOIN SOP10201 L on S.SOPNUMBE = L.SOPNUMBE and S.LNITMSEQ = L.LNITMSEQ and S.SOPTYPE = L.SOPTYPE Where S.SOPTYPE = 3 andL.SOPNUMBE = @Trx_Id";

            return db.LoadData<TrxModel, dynamic>(sql, new { Trx_ID = trxId }, ConnectionGP());
        }
        public static BomModel GetParentInfo(string trxId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@Trx_ID", trxId, DbType.String, ParameterDirection.Input);
            string sql = @"Select B.*, E.* , ISNULL(E.[Number of Organisms], 1) as NumberOfOrganisms  from BM10400 B left outer join 
                           EXT_ROYALTY_INFO E on B.ITEMNMBR = [Item Number] 
                           where TRX_ID = @Trx_ID and Component_ID = 0";

            using (IDbConnection connection = ConnectionGP())
            {
                BomModel parentModel = new BomModel();

                parentModel = connection.Query<BomModel>(sql, parameters).FirstOrDefault();

                return parentModel;

            }

        }
        public static List<LotRoyaltyEntryModel> GetAllLotRoyaltyEntries(string lot)
        {
            
            string sql = $"SELECT * FROM dbo.JLz_RoyaltyLotAtt  where LotNumber = @Lot";

            return db.LoadData<LotRoyaltyEntryModel, dynamic>(sql, new { Lot = lot }, ConnectionGP());
        }
        public static List<LotRoyaltyEntryModel> GetAllLotRoyaltyEntries(string itemNumber, string lot)
        {
          
            string sql = $"SELECT DISTINCT * FROM dbo.JLz_RoyaltyLotAtt  where RTRIM(ItemNumber) = @Item and RTRIM(LotNumber) = @Lot";

            return db.LoadData<LotRoyaltyEntryModel, dynamic>(sql, new { Lot = lot, Item = itemNumber } , ConnectionGP());
        }
        public static List<VendorModel> GetAllRoyaltyVendors()
        {
            
            
            string sql = $"SELECT VENDORID, [Royalty%], [MinComboRoyalty%]  FROM dbo.JLz_vRoyaltyVendorPercentage";

            return db.LoadData<VendorModel, dynamic>(sql, new { }, ConnectionGP());
                        
        }
        public static List<RoyaltySalesHistoryModel> GetRoyaltySalesHistoryByLot(string itemNumber, string lotNumber, string sopNumber)
        {
            string sql = $"SELECT * FROM JLz_RoyaltyLotHistory WHERE RTRIM(ItemNumber) = @ItemNumber and RTRIM(LotNumber) = @LotNumber and RTRIM(SOPNUMBE) = @SopNumber";

            return db.LoadData<RoyaltySalesHistoryModel, dynamic>(sql, new { ItemNumber = itemNumber, LotNumber = lotNumber, SopNumber = sopNumber }, ConnectionGP());

        }

        public static List<RawRoyaltyItemModel> GetAllRawRoidNumbers()
        {
            string sql = $"SELECT * FROM JLz_RoyaltyLotAtt WHERE ItemNumber like '100%' ORDER BY ItemNumber";

            return db.LoadData<RawRoyaltyItemModel, dynamic>(sql, 0 ,ConnectionGP());
        }

    
        public static void AddLotRoyaltyEntry(LotRoyaltyEntryModel model)
        {
            SqlConnection sqlConn = ConnectionGP();
            using (IDbConnection connection = sqlConn)
            {
                var p = new DynamicParameters();
                p.Add("@CurrentTransactionNumber", model.CurrentTransactionNumber);
                //p.Add("@OrigTransactionNumber", model.OrigTransactionNumber);
                p.Add("@Percentage", model.Percentage);
                p.Add("@ItemNumber", model.ItemNumber);
                p.Add("@LotNumber", model.LotNumber);
                p.Add("@VendorID", model.VendorID);
                p.Add("@NumberOfOrganisms", model.NumberOfOrgs, DbType.Int32);
                p.Add("@RLTSEQNUM", model.RLTSEQNUM);
                p.Add("@Description", model.Description);

              
                

                connection.Execute("[dbo].[JLz_spRoyaltyLotEntry_Insert]", p, commandType: CommandType.StoredProcedure);
            }
        }
        public static void DeleteInheritedLotRoyaltyEntry(string trxId)
        {
            //clear all royalties with current trxID

            SqlConnection sqlConn = ConnectionGP();
            using (IDbConnection connection = sqlConn)
            {
                var p = new DynamicParameters();
                p.Add("@CurrentTransactionNumber", trxId);
                //p.Add("@OrigTransactionNumber", model.OrigTransactionNumber);
              
                //p.Add("@ItemNumber", model.ItemNumber);
                //p.Add("@LotNumber", model.LotNumber);
                

                connection.Execute("[dbo].[JLz_spRoyaltyLotEntry_DeleteInherit]", p, commandType: CommandType.StoredProcedure);
            }

        }
        public static void AddInheritedLotRoyaltyEntry(LotRoyaltyEntryModel model)
        {
            SqlConnection sqlConn = ConnectionGP();
            using (IDbConnection connection = sqlConn)
            {
                var p = new DynamicParameters();
                p.Add("@CurrentTransactionNumber", model.CurrentTransactionNumber);
                //p.Add("@OrigTransactionNumber", model.OrigTransactionNumber);
                p.Add("@Percentage", model.Percentage);
                p.Add("@ItemNumber", model.ItemNumber);
                p.Add("@LotNumber", model.LotNumber);
                p.Add("@VendorID", model.VendorID);
                p.Add("@OGLot", model.OGLot);
                p.Add("@OGItem", model.OGItem);
                p.Add("@NumberOfOrganisms", model.NumberOfOrgs);
                
                

                connection.Execute("[dbo].[JLz_spRoyaltyLotEntry_InsertInherit]", p, commandType: CommandType.StoredProcedure);
            }
        }
        public static void AddOrEditLotRoyaltyEntry(LotRoyaltyEntryModel model)
        {
            SqlConnection sqlConn = ConnectionGP();
            using (IDbConnection connection = sqlConn)
            {
                var p = new DynamicParameters();
                p.Add("@CurrentTransactionNumber", model.CurrentTransactionNumber);
                //p.Add("@OrigTransactionNumber", model.OrigTransactionNumber);
                p.Add("@Percentage", model.Percentage);
                p.Add("@ItemNumber", model.ItemNumber);
                p.Add("@LotNumber", model.LotNumber);
                p.Add("@VendorID", model.VendorID);
                p.Add("@OGLot", model.OGLot);
                p.Add("@OGItem", model.OGItem);
                p.Add("@NumberOfOrganisms", model.NumberOfOrgs);
                p.Add("@RLTSEQNUM", model.RLTSEQNUM);
                p.Add("@Description", model.Description);



                connection.Execute("[dbo].[JLz_spRoyaltyLotEntry_Update]", p, commandType: CommandType.StoredProcedure);
            }
        }
        public static void CaptureRoyaltySalesHistory(string batchNumber)
        {
            SqlConnection sqlConn = ConnectionGP();
            using(IDbConnection connection = sqlConn)
            {
                var p = new DynamicParameters();
                p.Add("@BatchNum", batchNumber);

                connection.Execute("[dbo].[JLz_spRoyaltyLotHistory_BatchInsertBySales]", p, commandType: CommandType.StoredProcedure);
            }

        }
        public static int GetNumberOfOrgs(string itemNumber)
        {
            SqlConnection sqlConn = ConnectionGP();
            var parameters = new DynamicParameters();
            parameters.Add("@ItemNumber", itemNumber, DbType.String, ParameterDirection.Input);
            string sql = "IF EXISTS(SELECT DISTINCT  ISNULL([Number of Organisms], 0) as [NumberOfOrganisms] FROM EXT_ROYALTY_INFO WHERE RTRIM([Item Number]) like @ItemNumber)SELECT DISTINCT  ISNULL([Number of Organisms], 0) as [NumberOfOrganisms] FROM EXT_ROYALTY_INFO WHERE RTRIM([Item Number]) like @ItemNumber ELSE SELECT 1 as [NumberOfOrganisms]";


            using (IDbConnection connection = sqlConn)
            {
              
                var output = connection.QuerySingle<int>(sql, parameters);

                return output;
            }
        }
        public static int LookForDuplicateRoids(LotRoyaltyEntryModel model)
        {
            //Use before point of insert. Prompt user if output > 0.  This means OGLot already exists for this item lot combo.  
            SqlConnection sqlConn = ConnectionGP();
            var parameters = new DynamicParameters();
            parameters.Add("@OGLot", model.OGLot);
            parameters.Add("@LotNumber", model.LotNumber);
            parameters.Add("@ItemNumber", model.ItemNumber);

            string sql = "SELECT COUNT(OGLot) FROM JLz_RoyaltyLotAtt WHERE OGLot = @OGLot and LotNumber = @LotNumber and ItemNumber = @ItemNumber";

            using (IDbConnection connection = sqlConn)
            {
                int output = connection.QuerySingle<int>(sql, parameters);

                return output;
            }
        }
        public static int CatchUnwantedRoyaltyEntries(string trxId)
        {
            //Compare entries for Parent Component vs Entries for Components
            //May be more rows in components because duplicate entries from different component will only be applied to parent once
            //There should never be more rows in parent than components unless specialty royalty like MED000009
            SqlConnection sqlConn = ConnectionGP();
            using (IDbConnection connection = sqlConn)
            {
                var p = new DynamicParameters();
                p.Add("@TrxId", trxId);
                p.Add("@output", dbType: DbType.Int32, direction: ParameterDirection.Output);

                connection.Execute("[dbo].[JLz_spRoyaltyLotEntry_CatchUnwantedEntries]", p, commandType: CommandType.StoredProcedure);

                int output = p.Get<int>("@output");
                return output;
            }
        }
        
        public static decimal GetVendorPercentage(string vendorId)
        {
            SqlConnection sqlConn = ConnectionGP();
            var parameters = new DynamicParameters();
            parameters.Add("@VendorId", vendorId, DbType.String, ParameterDirection.Input);
            string sql = "SELECT [Royalty%] from dbo.JLz_vRoyaltyVendorPercentage where VENDORID = @VendorId";

            using (IDbConnection connection = sqlConn) 
            {
                //VendorModel vendorModel = new VendorModel();

                if (!string.IsNullOrEmpty(vendorId))
                {
                    decimal percentage = connection.QuerySingle<decimal>(sql, parameters);

                    return percentage;

                }
                else
                {
                    return 0;
                }
            }                               

        }

        public static void RemoveBmLotRoyaltiesByBatch(string batchNumber)
        {
            SqlConnection sqlConn = ConnectionGP();
            var parameters = new DynamicParameters();
            parameters.Add("@BatchNumber", batchNumber, DbType.String, ParameterDirection.Input);

            string sql = @"DELETE R FROM JLz_RoyaltyLotAtt R inner join BM10200 B on R.CurrentTransactionNumber = B.TRX_ID 
                           WHERE B.BACHNUMB = @BatchNumber";

            using (IDbConnection connection = sqlConn)
            {
                var results = connection.Execute(sql, parameters);
            }
        }
        public static void RemoveIvRoyaltiesByBatch(string batchNumber)
        {
            SqlConnection sqlConn = ConnectionGP();
            var parameters = new DynamicParameters();
            parameters.Add("@BatchNumber", batchNumber, DbType.String, ParameterDirection.Input);

            string sql = @"DELETE R FROM JLz_RoyaltyLotAtt R inner join IV10000 B on R.CurrentTransactionNumber = B.IVDOCNBR 
                           WHERE B.BACHNUMB = @BatchNumber";

            using (IDbConnection connection = sqlConn)
            {
                var results = connection.Execute(sql, parameters);
            }
        }


        public static void RemoveAllIvLotRoyalties(string trxId)
        {
            SqlConnection sqlConn = ConnectionGP();
            var parameters = new DynamicParameters();
            parameters.Add("@TrxID", trxId, DbType.String, ParameterDirection.Input);

            string sql = @"Delete R from JLz_RoyaltyLotAtt R inner join iv10002 T on T.IVDOCNBR = R.CurrentTransactionNumber
                           where  R.CurrentTransactionNumber = @TrxId ";
            using (IDbConnection connection = sqlConn)
            {
                var results = connection.Execute(sql, parameters);
            }

        }
        public static void RemoveIvLotRoyalty(string trxId, string itemNumber, decimal lineNum)
        {
            SqlConnection sqlConn = ConnectionGP();
            var parameters = new DynamicParameters();
            parameters.Add("@TrxID", trxId, DbType.String, ParameterDirection.Input);
            parameters.Add("@ItemNumber", itemNumber, DbType.String, ParameterDirection.Input);
            parameters.Add("@LineNumber", lineNum, DbType.Decimal, ParameterDirection.Input);

            string sql = @"Delete R from JLz_RoyaltyLotAtt R inner join iv10002 T on  T.ITEMNMBR = R.ItemNumber and T.SERLTNUM = R.LotNumber  and T.IVDOCNBR = R.CurrentTransactionNumber
--                           where R.ItemNumber = @ItemNumber and T.LNSEQNBR = @LineNumber  and R.CurrentTransactionNumber = @TrxId;";

            using (IDbConnection connection = sqlConn)
            {
                var results = connection.Execute(sql, parameters);

            }
        }
        public static void RemoveIvLotRoyalty(string trxId, string itemNumber, string lotNumber)
        {
            SqlConnection sqlConn = ConnectionGP();
            var parameters = new DynamicParameters();
            parameters.Add("@TrxID", trxId, DbType.String, ParameterDirection.Input);
            parameters.Add("@ItemNumber", itemNumber, DbType.String, ParameterDirection.Input);
            parameters.Add("@LotNumber", lotNumber, DbType.String, ParameterDirection.Input);

            string sql = @"Delete R from JLz_RoyaltyLotAtt R inner join iv10002 T on  T.ITEMNMBR = R.ItemNumber and T.SERLTNUM = R.LotNumber  and T.IVDOCNBR = R.CurrentTransactionNumber
--                           where R.ItemNumber = @ItemNumber and  R.LotNumber = @LotNumber and R.CurrentTransactionNumber = @TrxId;";

            using (IDbConnection connection = sqlConn)
            {
                var results = connection.Execute(sql, parameters);

            }
        }
        public static void RemovePopRoyaltiesByBatch(string batchNumber)
        {
            SqlConnection sqlConn = ConnectionGP();
            var parameters = new DynamicParameters();
            parameters.Add("@BatchNumber", batchNumber, DbType.String, ParameterDirection.Input);

            string sql = @"Delete R from JLz_RoyaltyLotAtt R inner join POP10300 P on P.POPRCTNM = R.CurrentTransactionNumber
                           where  P.BACHNUMB = @BatchNumber "; 

            using (IDbConnection connection = sqlConn)
            {
                var results = connection.Execute(sql, parameters);

            }

        }

        public static void RemoveAllPopLotRoyalties(string trxId)
        {
            SqlConnection sqlConn = ConnectionGP();
            var parameters = new DynamicParameters();
            parameters.Add("@TrxID", trxId, DbType.String, ParameterDirection.Input);
         
            string sql = @"Delete R from JLz_RoyaltyLotAtt R inner join POP10330 P on P.POPRCTNM = R.CurrentTransactionNumber
                           where  R.CurrentTransactionNumber = @TrxId ";

            using (IDbConnection connection = sqlConn)
            {
                var results = connection.Execute(sql, parameters);

            }

        }
        public static void RemovePopLotRoyalty(string trxId, string itemNumber, int lineNum)
        {
            SqlConnection sqlConn = ConnectionGP();
            var parameters = new DynamicParameters();
            parameters.Add("@TrxID", trxId, DbType.String, ParameterDirection.Input);
            parameters.Add("@ItemNumber", itemNumber, DbType.String, ParameterDirection.Input);
            parameters.Add("@LineNumber", lineNum, DbType.Int32, ParameterDirection.Input);

            string sql = @"Delete R from JLz_RoyaltyLotAtt R inner join POP10330 P on  P.ITEMNMBR = R.ItemNumber and P.POPRCTNM = R.CurrentTransactionNumber
                           where R.ItemNumber = @ItemNumber and R.CurrentTransactionNumber = @TrxId and P.RCPTLNNM = @LineNumber";

            using (IDbConnection connection = sqlConn)
            {
                var compId = connection.Execute(sql, parameters);

            }
        }
        public static void RemovePopLotRoyalty(string trxId, string itemNumber, string lotNumber)
        {
            SqlConnection sqlConn = ConnectionGP();
            var parameters = new DynamicParameters();
            parameters.Add("@TrxID", trxId, DbType.String, ParameterDirection.Input);
            parameters.Add("@ItemNumber", itemNumber, DbType.String, ParameterDirection.Input);
            parameters.Add("@LotNumber", lotNumber, DbType.String, ParameterDirection.Input);
            string sql = @"Delete R from JLz_RoyaltyLotAtt R inner join POP10330 P on P.SERLTNUM = OGLot and P.ITEMNMBR = R.ItemNumber 
                           where R.ItemNumber = @ItemNumber and R.LotNumber = @LotNumber and R.CurrentTransactionNumber = @TrxID";

            using (IDbConnection connection = sqlConn)
            {
                var compId = connection.Execute(sql, parameters);
                              
            }
        }
        
        public static int GetCompID(string itemNumber, string bmTrxId )
        {
            SqlConnection sqlConn = ConnectionGP();
            var parameters = new DynamicParameters();
            parameters.Add("@TrxID", bmTrxId, DbType.String, ParameterDirection.Input);
            parameters.Add("@ItemNumber", itemNumber, DbType.String, ParameterDirection.Input);
            string sql = "SELECT Component_ID FROM BM10400 WHERE TRX_ID = @TrxID and ITEMNMBR = @ItemNumber";

            using(IDbConnection connection = sqlConn)
            {
                var compId = connection.QuerySingle<int>(sql, parameters);

                return compId;
            }
        }

        public static int RoidExists(string roidNumber, string itemNumber, string vendorId)
        {
            //Used to only allow existing roids to be inserted during trx_entry
            int  roidExists = 0;
            SqlConnection sqlConn = ConnectionGP();
            var parameters = new DynamicParameters();
            parameters.Add("@RoidNumber", roidNumber);
            parameters.Add("@ItemNumber", itemNumber);
            parameters.Add("@VendorId", vendorId);

            string sql = "SELECT COUNT(OGLot) FROM JLZ_RoyaltyLotATT WHERE VendorId = @VendorId and ItemNumber = @ItemNumber and OGLot = @RoidNumber";

            using (IDbConnection connection = sqlConn)
            {
                 roidExists = connection.QueryFirstOrDefault<int>(sql, parameters);

            }
            if (roidExists > 0)
            {
                return 1;
            }
            else
            {
                return 0;
            }
         
        }

    }
}
