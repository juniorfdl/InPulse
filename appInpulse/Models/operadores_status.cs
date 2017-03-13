namespace Models
{
    using Infra.Base.Interface;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Web;

    public class operadores_status : IEntidadeBase
    {
        [Key]   
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("OPERADOR")]      
        public int id { get; set; }
        public string STATUS_ATUAL { get; set; }
        public Nullable<TimeSpan> TEMPO { get; set; } 
        [NotMapped]
        public string NOME { get; set; }
    }
}
