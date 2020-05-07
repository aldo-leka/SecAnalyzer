using JeffFerguson.Gepsio;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace SecAnalyzer.Controllers
{
    //Status
    //It's more worth it to read various reports and study businesses openly until I stumble in some interesting ones rather than
    //creating tools with preconceptions of what's considered good based on limited information.
    //Making this program is closed-minded as far as I can see now...since I can't even provide raw data because every company
    //is unique and the data that I would provide is biased on what I consider important.
    //Still interesting to do mindless formulas provided by gurus, but yeah...on hold for now.
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        [HttpGet]
        public string Get()
        {
            var xbrlDoc = new XbrlDocument();
            xbrlDoc.Load("https://www.sec.gov/Archives/edgar/data/874292/000144526020000011/aey-20191231.xml");
            ShowUnitsInDocument(xbrlDoc);
            ShowFactsInDocument(xbrlDoc);

            return JsonConvert.SerializeObject(xbrlDoc.XbrlFragments
                .SelectMany(frag => frag.Facts)
                .Select(fact => new DTOs.FactResult
                {
                    Name = fact.Name,
                    Value = (fact as Item).Value
                }), Formatting.Indented);
        }

        private static void ShowUnitsInDocument(XbrlDocument doc)
        {
            foreach (var currentFragment in doc.XbrlFragments)
            {
                ShowUnitsInFragment(currentFragment);
            }
        }

        private static void ShowUnitsInFragment(XbrlFragment currentFragment)
        {
            foreach (var currentUnit in currentFragment.Units)
            {
                ShowUnit(currentUnit);
            }
        }

        private static void ShowUnit(Unit currentUnit)
        {
            Debug.WriteLine("UNIT");
            Debug.WriteLine($"\tID  : {currentUnit.Id}");
            foreach (var currentMeasureQualifiedName in currentUnit.MeasureQualifiedNames)
            {
                Debug.WriteLine($"\tName: {currentMeasureQualifiedName.Namespace}:{currentMeasureQualifiedName.LocalName}");
            }
        }

        private static void ShowFactsInDocument(XbrlDocument doc)
        {
            foreach (var currentFragment in doc.XbrlFragments)
            {
                ShowFactsInFragment(currentFragment);
            }
        }

        private static void ShowFactsInFragment(XbrlFragment currentFragment)
        {
            foreach (var currentFact in currentFragment.Facts)
            {
                ShowFact(currentFact);
            }
        }

        private static void ShowFact(Fact fact)
        {
            Debug.WriteLine($"FACT {fact.Name}");
            if (fact is Item)
            {
                ShowItem(fact as Item);
            }
        }

        private static void ShowItem(Item item)
        {
            Debug.WriteLine("\tType     : Item");
            Debug.WriteLine($"\tNamespace: {item.Namespace}");
            Debug.WriteLine($"\tValue    : {item.Value}");
            Debug.WriteLine($"\tUnit ID  : {item.UnitRefName}");
            if (item.UnitRef != null)
                ShowUnit(item.UnitRef);
        }
    }
}