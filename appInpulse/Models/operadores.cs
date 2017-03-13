namespace Models
{
    using Infra.Base.Interface;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Web;

    public class operadores : IEntidadeBase
    {
        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        //public operadores()
        //{
        //    this.campanhas_clientes = new HashSet<campanhas_clientes>();
        //    this.campanhas_clientes1 = new HashSet<campanhas_clientes>();
        //    this.clientes = new HashSet<clientes>();
        //    this.login_ativo_receptivo = new HashSet<login_ativo_receptivo>();
        //} 
        [Key]   
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("CODIGO")]      
        public int id { get; set; }
        public string ATIVO { get; set; }
        public string NOME { get; set; }
        public string LOGIN { get; set; }
        public string EMAIL { get; set; }
        public string SENHA { get; set; }
        public Nullable<DateTime> EXPIRA_EM { get; set; }
        public string NIVEL { get; set; }
        public Nullable<int> HORARIO { get; set; }
        public string ALTERA_SENHA { get; set; }
        public string CODIGO_ERP { get; set; }
        public string EDITA_CONTATOS { get; set; }
        public string VISUALIZA_COMPRAS { get; set; }
        public string CADASTRO { get; set; }
        public Nullable<DateTime> ULTIMO_LOGIN_INI { get; set; }
        public Nullable<DateTime> ULTIMO_LOGIN_FIM { get; set; }
        public Nullable<DateTime> DATACAD { get; set; }
        public string CODTELEFONIA { get; set; }
        public Nullable<int> LOGADO { get; set; }
        public string AGENDA_LIG { get; set; }
        public string LIGA_REPRESENTANTE { get; set; }
        public string FILTRA_DDD { get; set; }
        public string FILTRA_ESTADO { get; set; }
        public string BANCO { get; set; }
        public string ASTERISK_RAMAL { get; set; }
        public string ASTERISK_USERID { get; set; }
        public string ASTERISK_LOGIN { get; set; }
        public string ASTERISK_SENHA { get; set; }
        public string CODEC { get; set; }               
    
        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<campanhas_clientes> campanhas_clientes { get; set; }
        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<campanhas_clientes> campanhas_clientes1 { get; set; }
        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<clientes> clientes { get; set; }
        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<login_ativo_receptivo> login_ativo_receptivo { get; set; }
    }
}
