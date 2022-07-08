namespace Battleship
{
    public class WaterShipShoots
    {
        
        private int waterShoot = 0;
        private int shipShoot = 0;

         public void AddWaterShoots()
        {
            this.waterShoot++;
        }
        public void AddShipShoots()
        {
            this.shipShoot++;
        }

        public int GetWaterShoots()
        {
            return this.waterShoot;
        }
        public int GetShipShoots()
        {
            return this.shipShoot;
        }
    }
    
}