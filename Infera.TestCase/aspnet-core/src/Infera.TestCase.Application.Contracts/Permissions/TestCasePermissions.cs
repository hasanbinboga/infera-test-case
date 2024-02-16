namespace Infera.TestCase.Permissions;

public static class TestCasePermissions
{
    public const string GroupName = "TestCase";

    //Add your own permission names. Example:
    
    public static class Buildings
    {
        public const string Default = GroupName + ".Buildings";
        public const string Create = Default + ".Create";
        public const string Edit = Default + ".Edit";
        public const string Delete = Default + ".Delete";
    }

    public static class Rooms
    {
        public const string Default = GroupName + ".Rooms";
        public const string Create = Default + ".Create";
        public const string Edit = Default + ".Edit";
        public const string Delete = Default + ".Delete";
    }


    public static class Warehouses
    {
        public const string Default = GroupName + ".Warehouses";
        public const string Create = Default + ".Create";
        public const string Edit = Default + ".Edit";
        public const string Delete = Default + ".Delete";
    }

    public static class ProductInventories
    {
        public const string Default = GroupName + ".ProductInventories";
        public const string Create = Default + ".Create";
        public const string Edit = Default + ".Edit";
        public const string Delete = Default + ".Delete";
    }

    public static class Issues
    {
        public const string Default = GroupName + ".Issues";
        public const string Create = Default + ".Create";
        public const string Edit = Default + ".Edit";
        public const string Delete = Default + ".Delete";
    }
    public static class Accountings
    {
        public const string Default = GroupName + ".Accountings";
        public const string Create = Default + ".Create";
        public const string Edit = Default + ".Edit";
        public const string Delete = Default + ".Delete";
    }
}
