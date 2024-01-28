using System;
using System.Collections.Generic;

public class StolenBottleManager {
   public event Action OnInventoryFull;
   
   private static StolenBottleManager _I = null;
   public static StolenBottleManager I
   {
      get {
         if (_I == null) {
            _I = new StolenBottleManager();
         }
         return _I;
      }
   }
   
   private List<Bottle> _stolenBottles = new List<Bottle>();

   public void AddBottle(Bottle bottle) {
      if (_stolenBottles == null) {
         _stolenBottles = new List<Bottle>();
      }
      
      _stolenBottles.Add(bottle);

      if (_stolenBottles.Count >= 4) {
         OnInventoryFull?.Invoke();
      }
   }

   public List<Bottle> GetBottleList() {
      return _stolenBottles;
   }

   public void ClearBottleList() {
      _stolenBottles.Clear();
   }
}
