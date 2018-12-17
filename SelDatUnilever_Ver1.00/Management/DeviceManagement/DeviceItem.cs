﻿using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SeldatMRMS.Management.RobotManagent.RobotUnityControl;

namespace SelDatUnilever_Ver1._00.Management.DeviceManagement
{
    public class DeviceItem
    {
        public enum TabletConTrol
        {
            TABLET_MACHINE = 10000,
            TABLET_FORKLIFT = 10001
        }
        public enum CommandRequest
        {
            CMD_DATA_ORDER_BUFFERTOMACHINE = 100,
            CMD_DATA_ORDER_RETURN = 100,
            CMD_DATA_ORDER_FORKLIFT = 101,
            CMD_DATA_STATE = 102
        }
        public class OrderItem
        {
            public OrderItem() { }
            public String OrderID;
            public int planID { get; set; }
            public int productID { get; set; }
            public int productDetailID { get; set; }
            public String typeRequest; // FL: ForkLift// BM: BUFFER MACHINE // PR: Pallet return
            public String activeDate;
            public int timeWorkID;
            public String palletStatus;
            public int palletId;
            public int updUsrId;
            public String dataRequest;
            public bool status = false; // chua hoan thanh
        }
        public string deviceID { get; set; } // dia chi Emei
        public string codeID { get; set; }
        public List<OrderItem> oneOrderList { get; set; }
        public int orderedAmount = 0;
        public int doneAmount = 0;
        public DeviceItem()
        {
            oneOrderList = new List<OrderItem>();
        }
        public void state(CommandRequest pCommandRequest, String data)
        {
            switch (pCommandRequest)
            {
                case CommandRequest.CMD_DATA_ORDER_BUFFERTOMACHINE:
                    break;
                case CommandRequest.CMD_DATA_ORDER_FORKLIFT:
                    break;
                case CommandRequest.CMD_DATA_STATE:
                    break;
            }
        }
        public void RemoveFirstOrder()
        {
            if (oneOrderList.Count > 0)
            {
                oneOrderList.RemoveAt(0);
            }
        }
        public void AddOrder(OrderItem hasOrder)
        {
            oneOrderList.Add(hasOrder);
        }
        public OrderItem GetOrder()
        {
            if (oneOrderList.Count > 0)
            {
                return oneOrderList[0];
            }
            return null;
        }
        public void ClearOrderList()
        {
            if (oneOrderList.Count > 0)
            {
                oneOrderList.Clear();
            }
        }
        public void rounter(String data)
        {

        }
        public void ParseDataOfForkLift(String dataReq)
        {
            OrderItem order = new OrderItem();
            JObject results = JObject.Parse(dataReq);
            order.productDetailID = (int)results["productDetailId"];
            order.productID = (int)results["productId"];
            order.timeWorkID = (int)results["timeWorkId"];
            order.palletStatus =(String)results["palletStatus"];
            order.dataRequest = dataReq;
            oneOrderList.Add(order);
        }

    }
}
