//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MediaAdvertisingApp.AppData
{
    using System;
    using System.Collections.Generic;
    
    public partial class Заказчик
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Заказчик()
        {
            this.Сотрудник = new HashSet<Сотрудник>();
        }
    
        public int Заказчик_ID { get; set; }
        public string Название_компании { get; set; }
        public Nullable<int> Длительность_рекламы { get; set; }
        public string Банковские_реквизиты { get; set; }
        public string Контактный_телефон { get; set; }
        public string Контактное_лицо { get; set; }
        public Nullable<int> Программа_ID { get; set; }
    
        public virtual Программа Программа { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Сотрудник> Сотрудник { get; set; }
    }
}
