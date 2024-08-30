namespace Supreme_Commander_Thorn
{
    public class ACU : Location
    {
        public enum AcuType { CybranTypeOne, UEFTypeOne, AeonTypeOne}
        public enum DamageState {BrandNew, FullyFunctional, SlightlyDamaged, NeedsRepair, DangerouslyBroken, Destroyed }
        public AcuType UnitType;
        public DamageState DmgState;
        public int Number;
        public Faction OwnerFaction;
        //Here will be the list of components and their state
        public ACU(string name, AcuType type, int number) : base(name) 
        {
            this.Number = number;
            this.UnitType = type;
            this.DmgState = DamageState.BrandNew;
        }
    }
}