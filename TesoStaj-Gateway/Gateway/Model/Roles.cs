using System.Runtime.Serialization;

namespace Gateway.Model
{
    public enum Roles
    {
        [EnumMember(Value = "Admin")]
        Admin,
        [EnumMember(Value = "Manager")]
        Manager,
        [EnumMember(Value = "Developer")]
        Developer
    }
}