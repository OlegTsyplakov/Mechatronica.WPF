using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mechatronica.WPF.Models
{
    public class MockData
    {   
        public static ConcurrentQueue<string> Cars { get; set; } = new ConcurrentQueue<string>(new List<string>() 
        { "Мондео", "Крета", "Приус", "УАЗик", "Вольво", "Фокус", "Октавия", "Запорожец", "Acura", "Datsun", "Honda", "Infiniti", "Lexus", "Mazda", "Mitsubishi", "Nissan", "Subaru", "Suzuki", "Toyota" });
        public static ConcurrentQueue<string> Persons { get; set; } = new ConcurrentQueue<string>(new List<string>() 
        { "Петр", "Василий", "Николай", "Марина", "Феодосий", "Карина", "Аарон", "Абдул", "Абдулла", "Август", "Августа", "Августин", "Августина", "Авдей", "Авдотья", "Авелина", "Авигея", "Авраам", "Аврора", "Автандил", "Агата" });
    }
}
