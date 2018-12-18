﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SelDatUnilever_Ver1._00.Management.ChargerCtrl.ChargerCtrl;

namespace SelDatUnilever_Ver1._00.Management.ChargerCtrl
{
    public class ChargerManagementService
    {

        private List<ChargerInfoConfig> CfChargerStationList;
        public ChargerCtrl ChargerStation_1;
        public ChargerCtrl ChargerStation_2;
        public ChargerCtrl ChargerStation_3;
        public ChargerCtrl ChargerStation_4;

        public ChargerManagementService()
        {
            LoadChargerConfigure();
            ChargerStation_1 = new ChargerCtrl(CfChargerStationList[0]);
            ChargerStation_2 = new ChargerCtrl(CfChargerStationList[1]);
            ChargerStation_3 = new ChargerCtrl(CfChargerStationList[2]);
            ChargerStation_4 = new ChargerCtrl(CfChargerStationList[3]);
        }
        public void LoadChargerConfigure()
        {
            string name = "Charger";
            String path = Path.Combine(System.IO.Directory.GetCurrentDirectory(), "Configure.xlsx");
            string constr = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" +
                            path +
                            ";Extended Properties='Excel 12.0 XML;HDR=YES;';";
            OleDbConnection con = new OleDbConnection(constr);
            OleDbCommand oconn = new OleDbCommand("Select * From [" + name + "$]", con);
            con.Open();

            OleDbDataAdapter sda = new OleDbDataAdapter(oconn);
            DataTable data = new DataTable();
            sda.Fill(data);
            CfChargerStationList = new List<ChargerInfoConfig>();
            foreach (DataRow row in data.Rows)
            {
                ChargerInfoConfig ptemp = new ChargerInfoConfig();
                ptemp.id = row.Field<ChargerId>("ID");
                ptemp.ip = row.Field<String>("IP");
                ptemp.port = row.Field<Int32>("Port");
                CfChargerStationList.Add(ptemp);
            }
            con.Close();
        }
    }
}