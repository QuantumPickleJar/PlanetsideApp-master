using Newtonsoft.Json;
using System.Collections.Generic;
using System.Collections;

namespace PsApp
{
    public class CharacterQueryResult //: IEnumerable
    {
        [JsonProperty("character_list")]
        public List<Character> Characters { get; set; }
        //[0:] Binding: 'Characters]' property not found on 'PsApp.CharacterQueryResult', target property: 'Xamarin.Forms.ListView.ItemsSource'

        [JsonProperty("returned")]
        public int Returned { get; set; }

        //public IEnumerator GetEnumerator()
        //{
        //    return ((IEnumerable)Characters).GetEnumerator();
        //}

    }
}


//using Newtonsoft.Json;
//using System.Collections.Generic;

//namespace PsApp
//{
//    public class CharacterQueryResult
//    {
//        [JsonProperty("character_list")]
//        public List<Character> Characters { get; set; }
//        //[0:] Binding: 'Characters]' property not found on 'PsApp.CharacterQueryResult', target property: 'Xamarin.Forms.ListView.ItemsSource'

//        [JsonProperty("returned")]
//        public int Returned { get; set; }

//        public List<Name> ReturnedNames { get; set; }
//    }
//}
