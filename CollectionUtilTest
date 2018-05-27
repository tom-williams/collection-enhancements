using System;
using System.Collections.Generic;
using iMotions.Myimotions.FileHandling;
using iMotions.Myimotions.Models;
using Xunit;

namespace iMotions.Collections
{
    public class CollectionUtilsTest
    {
        [Fact]
        public void GetSertDictionaryEntryWithNewableInsertsNewNoArgumentConstructedObjectIfKeyNotInDict()
        {
            Dictionary<String, Dictionary<int, StudyArchive.RespondentHolder>> crazyDictionary = 
                new Dictionary<string, Dictionary<int, StudyArchive.RespondentHolder>>();

            Dictionary<int, StudyArchive.RespondentHolder> result = 
                CollectionUtils.GetSertDictionaryEntry("what up", crazyDictionary);
            
            Assert.Equal(1, crazyDictionary.Count);
            Assert.Equal(result, crazyDictionary["what up"]);
        }
        
        [Fact]
        public void GetSertDictionaryEntryWithNewableReturnsObjectAssociatedWithKeyIfKeyInDict()
        {
            Dictionary<String, Dictionary<int, StudyArchive.RespondentHolder>> crazyDictionary = 
                new Dictionary<string, Dictionary<int, StudyArchive.RespondentHolder>>();
            
            Dictionary<int, StudyArchive.RespondentHolder> existing = 
                new Dictionary<int, StudyArchive.RespondentHolder>();
            existing.Add(1001, new StudyArchive.RespondentHolder() {Stim = new Stimuli(), Respondent = new Respondent()} );
            
            crazyDictionary.Add("what up", existing);
            
            Dictionary<int, StudyArchive.RespondentHolder> result = 
                CollectionUtils.GetSertDictionaryEntry("what up", crazyDictionary);
            
            Assert.Equal(existing, result);
            Assert.Single(crazyDictionary);
        }

        [Fact]
        public void GetSertDictionaryEntryWithObjectSupplierInsertsAndReturnsSupplierObjectWhenKeyNotPresent()
        {
            Dictionary<String, Dictionary<int, StudyArchive.RespondentHolder>> crazyDictionary = 
                new Dictionary<string, Dictionary<int, StudyArchive.RespondentHolder>>();

            Dictionary<int, StudyArchive.RespondentHolder> objectToAddIfNotPresent = 
                new Dictionary<int, StudyArchive.RespondentHolder>();
            objectToAddIfNotPresent.Add(
                1001, 
                new StudyArchive.RespondentHolder() {Stim = new Stimuli(), Respondent = new Respondent()} 
            );

            
            Dictionary<int, StudyArchive.RespondentHolder> result = 
                CollectionUtils.GetSertDictionaryEntry(
                    "what up", 
                    crazyDictionary, 
                    () => objectToAddIfNotPresent);
            
            Assert.Equal(objectToAddIfNotPresent, result);
            Assert.Single(crazyDictionary);
        }
        
        [Fact]
        public void GetSertDictionaryEntryWithObjectSupplierReturnsValueAssociatedWithKeyIfKeyInDict()
        {
            Dictionary<String, Dictionary<int, StudyArchive.RespondentHolder>> crazyDictionary = 
                new Dictionary<string, Dictionary<int, StudyArchive.RespondentHolder>>();

            
            Dictionary<int, StudyArchive.RespondentHolder> existing = 
                new Dictionary<int, StudyArchive.RespondentHolder>();
            existing.Add(
                1005, 
                new StudyArchive.RespondentHolder() {Stim = new Stimuli(), Respondent = new Respondent() { Age = 15} } 
            );
            
            crazyDictionary.Add("what up", existing);
            
            
            Dictionary<int, StudyArchive.RespondentHolder> objectToAddIfNotPresent = 
                new Dictionary<int, StudyArchive.RespondentHolder>();
            objectToAddIfNotPresent.Add(
                1001, 
                new StudyArchive.RespondentHolder() {Stim = new Stimuli(), Respondent = new Respondent()} 
            );

            
            Dictionary<int, StudyArchive.RespondentHolder> result = 
                CollectionUtils.GetSertDictionaryEntry(
                    "what up", 
                    crazyDictionary, 
                    () => objectToAddIfNotPresent);
            
            Assert.Equal(existing, result);
            Assert.Single(crazyDictionary);
        }
        
        [Fact]
        public void GetSertEntryListWithObjectSupplierInsertsAndReturnsSupplierObjectWhenNotPresent()
        {            
            List<int> ints = new List<int>() {1, 2, 3};

            int myInt = 5;
            var result = CollectionUtils.GetSertListEntry(ints, i => i == 5, () => myInt);
            
            Assert.Equal(result, myInt);
            Assert.Contains(ints, i => i == myInt);
        }

        [Fact]
        public void GetSertEntryListWithObjectSupplierReturnsExistingObjectIfMatchingFunctionReturnsTrue()
        {
            List<string> strings = new List<string>() {"This is great", "dont match me", "another entry"};

            string objectToInsertIfNoMatch = "This is not great";

            var result = CollectionUtils.GetSertListEntry(
                strings,
                str => str.Contains("This") && str.Contains("great"),
                () => objectToInsertIfNoMatch);
            
            Assert.NotEqual(result, objectToInsertIfNoMatch);
            Assert.Equal("This is great", result);
            Assert.DoesNotContain(strings, str => str.Equals(objectToInsertIfNoMatch));
            Assert.Equal(3, strings.Count);
        }

        [Fact]
        public void MapFirstOrDefaultShouldMapAndReturnMatchFirstMatchingEntry()
        {
            List<string> numberStrings = new List<string>() { "1", "23", "2", "26", "5"};

            int result = CollectionUtils.MapFirstOrDefault(
                numberStrings,
                str => str.ToUpper().StartsWith("2"),
                Int32.Parse,
                () => -1
            );
            
            Assert.Equal(23, result);
        }

        [Fact]
        public void MapFirstOrDefaultShouldReturnDefaultValueIfNoObjectsInListReturnTrueForMatchingFunction()
        {
            List<string> numberStrings = new List<string>() { "1", "23", "2", "26", "5"};

            int result = CollectionUtils.MapFirstOrDefault(
                numberStrings,
                str => str.ToUpper().StartsWith("6"),
                Int32.Parse,
                () => -1
            );
            
            Assert.Equal(result, -1);
        }
        
        [Fact]
        public void MapFirstOrDefaultShouldReturnDefaultValueIfListIsEmpty()
        {
            List<string> numberStrings = new List<string>();

            int result = CollectionUtils.MapFirstOrDefault(
                numberStrings,
                str => str.ToUpper().StartsWith("1"),
                Int32.Parse,
                () => -1
            );
            
            Assert.Equal(result, -1);
        }

        [Fact]
        public void MapFirstOrDefaultShouldReturnDefaultValueIfListIsNull()
        {
            List<string> numberStrings = null;

            int result = CollectionUtils.MapFirstOrDefault(
                numberStrings,
                str => str.ToUpper().StartsWith("1"),
                Int32.Parse,
                () => -1
            );
            
            Assert.Equal(result, -1);
        }

        [Fact]
        public void MapOrDefaultShouldMapAllValuesInListWithProvidedMappingFunction()
        {
            List<string> numberStrings = new List<string>() { "1", "23", "2", "26", "5"};

            List<int> result = CollectionUtils.MapOrDefault(
                numberStrings,
                Int32.Parse,
                () => new List<int>()
            );
            
            Assert.Equal(5, result.Count);
            Assert.Equal(result, new List<int>() { 1, 23, 2, 26, 5});
        }

        [Fact]
        public void MapOrDefaultShouldDefaultValueIfListIsEmpty()
        {
            List<string> numberStrings = new List<string>();

            List<int> result = CollectionUtils.MapOrDefault(
                numberStrings,
                Int32.Parse,
                () => new List<int>()
            );
            
            Assert.Empty(result);
        }

        [Fact]
        public void MapOrDefaultShouldDefaultValueIfListIsNull()
        {
            List<string> numberStrings = null;

            List<int> result = CollectionUtils.MapOrDefault(
                numberStrings,
                Int32.Parse,
                () => new List<int>()
            );
            
            Assert.Empty(result);
        }

        [Fact]
        public void OrderOrDefaultShouldReturnNewListOrderedByOrderingFunction()
        {
            List<string> numberStrings = new List<string>() { "1", "23", "2", "26", "5"};

            List<string> result = CollectionUtils.OrderOrDefault(
                numberStrings, 
                Int32.Parse, 
                () => new List<string>());

            Assert.Equal(5, result.Count);
            Assert.Equal(result, new List<string>() {"1", "2", "5", "23", "26"});
            Assert.NotEqual(numberStrings, result);

        }
    }
}
