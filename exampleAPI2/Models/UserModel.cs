using Microsoft.AspNetCore.SignalR;

namespace exampleAPI2.Models
{
    public class User
    {
        public int? id { get; set; } = null;
        public string name { get; set; } = "";
        public string email { get; set; } = "";
        public DateTime? fechaCreacion { get; set; } = null;
        public string? imgURL { get; set; } = "";
        public bool activo { get; set; } = false;
    }

    public class ResponseUser
    {
        public string? textResponse { get; set; } = null;
        public object? data { get; set; } = null;
    }
}
