
using System.Collections.Concurrent;
using System.Collections.Generic;


namespace Mechatronica.WPF.Models
{
    public class MockData
    {   
        public static ConcurrentQueue<string> Cars { get; set; } = new ConcurrentQueue<string>(new List<string>() 
        { "Мондео", "Крета", "Приус", "УАЗик", "Вольво", "Фокус", "Октавия", "Запорожец", "Acura", "Datsun", "Honda", "Infiniti", "Lexus", "Mazda", "Mitsubishi", "Nissan", "Subaru", "Suzuki", "Toyota","Saab", "SAIC Motor", 
            "SAIPA", "Saleen", "Saturn", "Scania", "Scion", "SEAT", "Setra", "Shelby", "Simca", "Singer", "Sisu Auto", "Škoda", "Skywell", "Smart", "Soueast", "Spirra", "Spyker", "SRT", "SsangYong", "SSC", "Sterling", "Studebaker",
            "Subaru", "Suffolk", "Suzuki", "Paccar", "Pagani", "Panhard", "Panoz", "Pegaso", "Perodua", "Peterbilt", "Peugeot", "PGO", "Pierce-Arrow", "Pininfarina", "Plymouth", "Polestar", "Pontiac", "", "Porsche", "Praga", "Premier", "Prodrive", "Proton" });
        public static ConcurrentQueue<string> Persons { get; set; } = new ConcurrentQueue<string>(new List<string>() 
        { "Петр", "Василий", "Николай", "Марина", "Феодосий", "Карина", "Аарон", "Абдул", "Абдулла", "Август", "Августа", "Августин", "Августина", "Авдей", "Авдотья", "Авелина", "Авигея", "Авраам", "Аврора", "Автандил", "Агата"
            ,"Аким", "Аксён", "Аксинья", "Акулина", "Алан", "Алана", "Алдона", "Алевтин", "Алевтина", "Александр", "Александра", "Александрина", "Алексей", "Алексий", "Ален", "Алёна", "Алеся", "Али", "Алика", "Алико", "Алима", 
            "Алина", "Алира", "Алиса", "Алихан", "Алия", "Алла", "Алмаз", "Алоис", "Алсу", "Алтжин", "Альба", "Альберт", "Альберта", "Альбина", "Альвиан", "Альвина", "Альжбета", "Альфия"});
    }
}
