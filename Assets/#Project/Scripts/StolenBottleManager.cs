using System;
using System.Collections.Generic;

public class StolenBottleManager {
   public event Action OnInventoryFull;
   
   private static StolenBottleManager _I = null;
   public static StolenBottleManager I
   {
      get {
         if (I == null) {
            _I = new StolenBottleManager();
         }
         return I;
      }
   }
   
   private List<Bottle> _stolenBottles;

   public void AddBottle(Bottle bottle) {
      _stolenBottles.Add(bottle);

      if (_stolenBottles.Count >= 4) {
         OnInventoryFull?.Invoke();
      }
   }

   public List<Bottle> GetBottleList() {
      return _stolenBottles;
   }
}
