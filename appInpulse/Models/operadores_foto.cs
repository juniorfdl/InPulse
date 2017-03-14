namespace Models
{
    using Infra.Base.Interface;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Web;

    public class operadores_foto : IEntidadeBase
    {
        [Key]   
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("OPERADOR")]      
        public int id { get; set; }
        public byte[] FOTO { get; set; }
        
    }
}
