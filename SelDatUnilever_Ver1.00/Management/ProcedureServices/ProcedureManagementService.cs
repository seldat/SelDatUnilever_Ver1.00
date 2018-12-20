﻿using SeldatMRMS.Management.DoorServices;
using SeldatMRMS.Management.RobotManagent;
using SeldatMRMS.Management.TrafficManager;
using SelDatUnilever_Ver1._00.Management.ChargerCtrl;
using System;
using static SeldatMRMS.DBProcedureService;
using static SelDatUnilever_Ver1._00.Management.DeviceManagement.DeviceItem;

namespace SeldatMRMS
{
    public class ProcedureManagementService:RegisterProcedureService
    {
        protected DoorManagementService doorService;
        protected ChargerManagementService chargerService;
        protected TrafficManagementService trafficService;
        public ProcedureManagementService()
        {
            doorService = new DoorManagementService();
        }
        public void RegistryTrafficService(TrafficManagementService trafficService)
        {
            this.trafficService = trafficService;
        }
        public void Register(ProcedureItemSelected ProcedureItem, RobotUnity robot,OrderItem orderItem)
        {
            
            switch(ProcedureItem)
            {
                case ProcedureItemSelected.PROCEDURE_FORLIFT_TO_BUFFER:
                        ProcedureForkLiftToBuffer procfb = new ProcedureForkLiftToBuffer(robot, doorService, trafficService);
                        ProcedureDataItems profbDataItems = new ProcedureDataItems();
                        profbDataItems.StartTaskTime = DateTime.Now;
                        RegisterProcedureItem itemprocfb = new RegisterProcedureItem() { item = procfb, robot = robot, procedureDataItems = profbDataItems };
                        procfb.ReleaseProcedureHandler += ReleaseProcedureItemHandler;
                        RegisterProcedureItemList.Add(itemprocfb);
                    break;
                case ProcedureItemSelected.PROCEDURE_BUFFER_TO_MACHINE:
                        ProcedureBufferToMachine procbm = new ProcedureBufferToMachine(robot, trafficService);
                        ProcedureDataItems prcobmDataItems = new ProcedureDataItems();
                        prcobmDataItems.StartTaskTime = DateTime.Now;
                        RegisterProcedureItem itemprocbm = new RegisterProcedureItem() { item = procbm, robot = robot, procedureDataItems = prcobmDataItems };
                        procbm.ReleaseProcedureHandler += ReleaseProcedureItemHandler;
                        RegisterProcedureItemList.Add(itemprocbm);
                    break;
                case ProcedureItemSelected.PROCEDURE_BUFFER_TO_RETURN: 
                        ProcedureBufferToReturn procbr = new ProcedureBufferToReturn(robot, trafficService);
                        ProcedureDataItems prcobrDataItems = new ProcedureDataItems();
                        prcobrDataItems.StartTaskTime = DateTime.Now;
                        RegisterProcedureItem itemprocbr = new RegisterProcedureItem() { item = procbr, robot = robot, procedureDataItems = prcobrDataItems };
                        procbr.ReleaseProcedureHandler += ReleaseProcedureItemHandler;
                        RegisterProcedureItemList.Add(itemprocbr);
                    break;
                case ProcedureItemSelected.PROCEDURE_MACHINE_TO_RETURN:
                        ProcedureMachineToReturn procmr = new ProcedureMachineToReturn(robot, trafficService);
                        ProcedureDataItems prcomrDataItems = new ProcedureDataItems();
                        prcomrDataItems.StartTaskTime = DateTime.Now;
                        RegisterProcedureItem itemprocmr = new RegisterProcedureItem() { item = procmr, robot = robot, procedureDataItems = prcomrDataItems };
                        procmr.ReleaseProcedureHandler += ReleaseProcedureItemHandler;
                        RegisterProcedureItemList.Add(itemprocmr);
                    break;
                case ProcedureItemSelected.PROCEDURE_ROBOT_TO_CHARGE:
                        ProcedureRobotToCharger procrc = new ProcedureRobotToCharger(robot, chargerService,robot.properties.chargeID);
                        ProcedureDataItems procrcDataItems = new ProcedureDataItems();
                        procrcDataItems.StartTaskTime = DateTime.Now;
                        RegisterProcedureItem itemprocrc = new RegisterProcedureItem() { item = procrc, robot = robot, procedureDataItems = procrcDataItems};
                        procrc.ReleaseProcedureHandler += ReleaseProcedureItemHandler;
                        RegisterProcedureItemList.Add(itemprocrc);
                    break;
                case ProcedureItemSelected.PROCEDURE_ROBOT_TO_READY:
                        ProcedureRobotToReady procrr = new ProcedureRobotToReady(robot);
                        ProcedureDataItems procrrDataItems = new ProcedureDataItems();
                        procrrDataItems.StartTaskTime = DateTime.Now;
                        RegisterProcedureItem itemprocrr = new RegisterProcedureItem() { item = procrr, robot = robot, procedureDataItems = procrrDataItems };
                        procrr.ReleaseProcedureHandler += ReleaseProcedureItemHandler;
                        RegisterProcedureItemList.Add(itemprocrr);
                    break;

            }
        }
    }
}
