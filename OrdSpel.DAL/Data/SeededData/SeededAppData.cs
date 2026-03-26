using System;
using System.Collections.Generic;
using System.Text;

namespace OrdSpel.DAL.Data.SeededData
{
    public class SeededAppData
    {
        //seeda kategorier
        public static async Task SeedCategoriesAsync(AppDbContext context)
        {
            //lägg till kategorier i listan efterhand
            var categoryName = new List<string> { "Länder" };

            //om det inte finns en category med ett namn från listan ovan, skapa:
            foreach (var name in categoryName)
            {
                if (!context.Categories.Any(c => c.Name == name))
                {
                    context.Categories.Add(new Models.Category { Name = name });
                }
            }

            await context.SaveChangesAsync();
        }

        //seeda länder
        public static async Task SeedCountriesAsync(AppDbContext context)
        {
            //hitta kategorin
            var category = context.Categories.FirstOrDefault(c => c.Name == "Länder");

            //om kategorin inte finns, avbryt
            if (category == null)
            {
                return;
            }

            var countries = new List<string>
            {
                "Afghanistan",
"Albanien",
"Algeriet",
"Andorra",
"Angola",
"Antigua och Barbuda",
"Argentina",
"Armenien",
"Australien",
"Azerbajdzjan",
"Bahamas",
"Bahrain",
"Bangladesh",
"Barbados",
"Belarus",
"Vitryssland",
"Belgien",
"Belize",
"Benin",
"Bhutan",
"Bolivia",
"Bosnien och Hercegovina",
"Botswana",
"Brasilien",
"Brunei",
"Bulgarien",
"Burkina Faso",
"Burundi",
"Centralafrikanska republiken",
"Chile",
"Colombia",
"Comorerna",
"Demokratiska republiken Kongo",
"Danmark",
"Djibouti",
"Dominica",
"Dominikanska republiken",
"Ecuador",
"Egypten",
"El Salvador",
"Ekvatorialguinea",
"Eritrea",
"Estland",
"Eswatini",
"Swaziland",
"Etiopien",
"Fiji",
"Filippinerna",
"Finland",
"Frankrike",
"Gabon",
"Gambia",
"Georgien",
"Ghana",
"Grekland",
"Grenada",
"Guatemala",
"Guinea",
"Guinea-Bissau",
"Guyana",
"Haiti",
"Honduras",
"Indien",
"Indonesien",
"Irak",
"Iran",
"Irland",
"Island",
"Israel",
"Italien",
"Jamaica",
"Japan",
"Jordanien",
"Kambodja",
"Kamerun",
"Kanada",
"Kap Verde",
"Kazakstan",
"Kenya",
"Kina",
"Kirgizistan",
"Kiribati",
"Kolombia",
"Komorerna",
"Kongo-Brazzaville",
"Kosovo",
"Kroatien",
"Kuba",
"Kuwait",
"Laos",
"Lettland",
"Libanon",
"Liberia",
"Libyen",
"Liechtenstein",
"Litauen",
"Luxemburg",
"Madagaskar",
"Malawi",
"Malaysia",
"Maldiverna",
"Mali",
"Malta",
"Marocko",
"Marshallöarna",
"Mauretanien",
"Mauritius",
"Mexiko",
"Mikronesien",
"Moldavien",
"Monaco",
"Mongoliet",
"Montenegro",
"Mozambique",
"Myanmar",
"Burma",
"Namibia",
"Nauru",
"Nepal",
"Nicaragua",
"Nederländerna",
"New Zealand",
"Nya Zeeland",
"Niger",
"Nigeria",
"Nordkorea",
"Nordmakedonien",
"Norge",
"Oman",
"Pakistan",
"Palau",
"Panama",
"Papua Nya Guinea",
"Paraguay",
"Peru",
"Polen",
"Portugal",
"Qatar",
"Republiken Kongo",
"Rumänien",
"Rwanda",
"Ryssland",
"Saint Kitts och Nevis",
"Saint Lucia",
"Saint Vincent och Grenadinerna",
"Samoa",
"San Marino",
"Saudiarabien",
"Senegal",
"Serbien",
"Seychellerna",
"Sierra Leone",
"Singapore",
"Slovakien",
"Slovenien",
"Somalia",
"Spanien",
"Sri Lanka",
"Sudan",
"Surinam",
"Sverige",
"Schweiz",
"Syrien",
"Tadzjikistan",
"Taiwan",
"Tanzania",
"Thailand",
"Togo",
"Tonga",
"Trinidad och Tobago",
"Tunisien",
"Turkiet",
"Turkmenistan",
"Tuvalu",
"Uganda",
"Ukraina",
"Ungern",
"Uruguay",
"Uzbekistan",
"Vanuatu",
"Vatikanstaten",
"Venezuela",
"Vietnam",
"Vitryssland",
"Yemen",
"Jemen",
"Zambia",
"Zimbabwe"
            };

            foreach(var country in countries)
            {
                if (!context.Words.Any(w => w.Text.ToLower() == country && w.CategoryId == category.Id))
                {
                    context.Words.Add(new Models.Word
                    {
                        Text = country.ToLower(),
                        CategoryId = category.Id,
                        IsHard = false
                    });
                }
            }

            await context.SaveChangesAsync();
        }
    }
}
