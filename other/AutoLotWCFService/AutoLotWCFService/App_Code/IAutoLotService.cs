using AutoLotWCFService.App_Code;
using System.Collections.Generic;
using System.ServiceModel;

[ServiceContract]
public interface IAutoLotService
{
    [OperationContract]
    void InsertCar(string make, string color, string petname);

    [OperationContract(Name = "InsertCarWithDetails")]
    void InsertCar(InventoryRecord car);

    [OperationContract]
    List<InventoryRecord> GetInventory();
}
