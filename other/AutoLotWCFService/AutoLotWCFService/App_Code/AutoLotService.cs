using System;
using System.Collections.Generic;
using AutoLotWCFService.App_Code;
using AutoLotDAL.Repos;
using AutoLotDAL.Models;
using System.Linq;

public class AutoLotService : IAutoLotService
{
    public List<InventoryRecord> GetInventory()
    {
        var repo = new InventoryRepo();
        var records = repo.GetAll()
            .Select(r => new InventoryRecord
            {
                ID = r.Id,
                Make = r.Make,
                Color = r.Color,
                PetName = r.PetName
            }).ToList();
        return records;
    }

    public void InsertCar(string make, string color, string petname)
    {
        var repo = new InventoryRepo();
        repo.Add(new Inventory
        {
            Color = color,
            Make = make,
            PetName = petname
        });
        repo.Dispose();
    }

    public void InsertCar(InventoryRecord car)
    {
        var repo = new InventoryRepo();
        repo.Add(new Inventory
        {
            Color = car.Color,
            Make = car.Make,
            PetName = car.PetName
        });
        repo.Dispose();
    }
}
