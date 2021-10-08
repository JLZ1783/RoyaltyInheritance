using DynamicsGPAddin1.Models;
using Microsoft.Dexterity.Applications.DynamicsDictionary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DynamicsGPAddin1
{
    public class Controller
    {
        private static Model model = new Model();
        private static readonly Controller instance = new Controller();
        private static TrxModel trxModel = new TrxModel();

        public Model Model
        {
            get
            {
                return Controller.model;
            }
        }

        public static Controller Instance
        {
            get
            {
                return Controller.instance;
            }
        }
        public TrxModel TrxModel
        {
            get 
            {
                return Controller.trxModel;
            }
        }
        public void SetDeleteType(short deleteType)
        {
            model.DeleteType = deleteType;
        }
        public void SetTrxId(string trxId)
        {
            model.TrxID = trxId;
        }
        public void SetWindowType(short type)
        {
            model.WindowType = type;
       
        }

        public void SetItemNumber()
        {
            switch (model.WindowType)
            {
                case 1:

                    break;
            }
        }

        public void SetTrxBatchNumber(string batchNumber)
        {
            trxModel.BatchNumber = batchNumber;
        }


        public void SetConnectionInfo()
        {
            Microsoft.Dexterity.Applications.DynamicsDictionary.SyBackupRestoreForm backup;
            backup = Microsoft.Dexterity.Applications.Dynamics.Forms.SyBackupRestore;
            model.GPServer = backup.Functions.GetServerNameWithInstance.Invoke();
            model.GPUserID = Microsoft.Dexterity.Applications.Dynamics.Globals.UserId.Value;
            model.GPPassword = Microsoft.Dexterity.Applications.Dynamics.Globals.SqlPassword.Value;
            model.GPSystemDB = Microsoft.Dexterity.Applications.Dynamics.Globals.SystemDatabaseName.Value;
            model.GPCompanyDB = Microsoft.Dexterity.Applications.Dynamics.Globals.IntercompanyId.Value;
        }

    }
}
