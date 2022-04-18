using System.ComponentModel.DataAnnotations.Schema;

namespace RestASPNET.Model.Base
{
    public class BaseEntity
    {
        [Column("id")]
        public long Id { get; set; }
    }
}
