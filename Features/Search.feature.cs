﻿// ------------------------------------------------------------------------------
//  <auto-generated>
//      This code was generated by Reqnroll (https://www.reqnroll.net/).
//      Reqnroll Version:2.0.0.0
//      Reqnroll Generator Version:2.0.0.0
// 
//      Changes to this file may cause incorrect behavior and will be lost if
//      the code is regenerated.
//  </auto-generated>
// ------------------------------------------------------------------------------
#region Designer generated code
#pragma warning disable
namespace ReqnrollPlaywrightRestSharpDemo.Features
{
    using Reqnroll;
    using System;
    using System.Linq;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Reqnroll", "2.0.0.0")]
    [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [NUnit.Framework.TestFixtureAttribute()]
    [NUnit.Framework.DescriptionAttribute("Search product")]
    public partial class SearchProductFeature
    {
        
        private global::Reqnroll.ITestRunner testRunner;
        
        private static string[] featureTags = ((string[])(null));
        
        private static global::Reqnroll.FeatureInfo featureInfo = new global::Reqnroll.FeatureInfo(new System.Globalization.CultureInfo("en-US"), "Features", "Search product", "    As a customer\r\n    I want be able to search for products\r\n    So that I can f" +
                "ind specific items quickly", global::Reqnroll.ProgrammingLanguage.CSharp, featureTags);
        
#line 1 "Search.feature"
#line hidden
        
        [NUnit.Framework.OneTimeSetUpAttribute()]
        public static async System.Threading.Tasks.Task FeatureSetupAsync()
        {
        }
        
        [NUnit.Framework.OneTimeTearDownAttribute()]
        public static async System.Threading.Tasks.Task FeatureTearDownAsync()
        {
        }
        
        [NUnit.Framework.SetUpAttribute()]
        public async System.Threading.Tasks.Task TestInitializeAsync()
        {
            testRunner = global::Reqnroll.TestRunnerManager.GetTestRunnerForAssembly(featureHint: featureInfo);
            if (((testRunner.FeatureContext != null) 
                        && (testRunner.FeatureContext.FeatureInfo.Equals(featureInfo) == false)))
            {
                await testRunner.OnFeatureEndAsync();
            }
            if ((testRunner.FeatureContext == null))
            {
                await testRunner.OnFeatureStartAsync(featureInfo);
            }
        }
        
        [NUnit.Framework.TearDownAttribute()]
        public async System.Threading.Tasks.Task TestTearDownAsync()
        {
            await testRunner.OnScenarioEndAsync();
            global::Reqnroll.TestRunnerManager.ReleaseTestRunner(testRunner);
        }
        
        public void ScenarioInitialize(global::Reqnroll.ScenarioInfo scenarioInfo)
        {
            testRunner.OnScenarioInitialize(scenarioInfo);
            testRunner.ScenarioContext.ScenarioContainer.RegisterInstanceAs<NUnit.Framework.TestContext>(NUnit.Framework.TestContext.CurrentContext);
        }
        
        public async System.Threading.Tasks.Task ScenarioStartAsync()
        {
            await testRunner.OnScenarioStartAsync();
        }
        
        public async System.Threading.Tasks.Task ScenarioCleanupAsync()
        {
            await testRunner.CollectScenarioErrorsAsync();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Search for a product that does exist")]
        [NUnit.Framework.CategoryAttribute("ui")]
        [NUnit.Framework.CategoryAttribute("api")]
        [NUnit.Framework.TestCaseAttribute("\"t-shirt\"", "\"Pure Cotton V-Neck T-Shirt\"", "\"Rs. 1299\"", null)]
        public async System.Threading.Tasks.Task SearchForAProductThatDoesExist(string searchterm, string description, string price, string[] exampleTags)
        {
            string[] @__tags = new string[] {
                    "ui",
                    "api"};
            if ((exampleTags != null))
            {
                @__tags = System.Linq.Enumerable.ToArray(System.Linq.Enumerable.Concat(@__tags, exampleTags));
            }
            string[] tagsOfScenario = @__tags;
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            argumentsOfScenario.Add("searchterm", searchterm);
            argumentsOfScenario.Add("description", description);
            argumentsOfScenario.Add("price", price);
            global::Reqnroll.ScenarioInfo scenarioInfo = new global::Reqnroll.ScenarioInfo("Search for a product that does exist", null, tagsOfScenario, argumentsOfScenario, featureTags);
#line 8
    this.ScenarioInitialize(scenarioInfo);
#line hidden
            if ((global::Reqnroll.TagHelper.ContainsIgnoreTag(scenarioInfo.CombinedTags) || global::Reqnroll.TagHelper.ContainsIgnoreTag(featureTags)))
            {
                testRunner.SkipScenario();
            }
            else
            {
                await this.ScenarioStartAsync();
#line 9
    await testRunner.GivenAsync("Jeff is authenticated and authorised for searching products", ((string)(null)), ((global::Reqnroll.Table)(null)), "Given ");
#line hidden
#line 10
    await testRunner.WhenAsync(string.Format("Jeff searches for {0}", searchterm), ((string)(null)), ((global::Reqnroll.Table)(null)), "When ");
#line hidden
#line 11
    await testRunner.ThenAsync(string.Format("An item with description {0} and a price {1} should be returned", description, price), ((string)(null)), ((global::Reqnroll.Table)(null)), "Then ");
#line hidden
            }
            await this.ScenarioCleanupAsync();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Search for a product that does not exist")]
        [NUnit.Framework.CategoryAttribute("ui")]
        [NUnit.Framework.CategoryAttribute("api")]
        public async System.Threading.Tasks.Task SearchForAProductThatDoesNotExist()
        {
            string[] tagsOfScenario = new string[] {
                    "ui",
                    "api"};
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            global::Reqnroll.ScenarioInfo scenarioInfo = new global::Reqnroll.ScenarioInfo("Search for a product that does not exist", null, tagsOfScenario, argumentsOfScenario, featureTags);
#line 19
    this.ScenarioInitialize(scenarioInfo);
#line hidden
            if ((global::Reqnroll.TagHelper.ContainsIgnoreTag(scenarioInfo.CombinedTags) || global::Reqnroll.TagHelper.ContainsIgnoreTag(featureTags)))
            {
                testRunner.SkipScenario();
            }
            else
            {
                await this.ScenarioStartAsync();
#line 20
    await testRunner.GivenAsync("Jeff is authenticated and authorised for searching products", ((string)(null)), ((global::Reqnroll.Table)(null)), "Given ");
#line hidden
#line 21
    await testRunner.WhenAsync("Jeff searches for \"does not exist\"", ((string)(null)), ((global::Reqnroll.Table)(null)), "When ");
#line hidden
#line 22
    await testRunner.ThenAsync("No items should be returned", ((string)(null)), ((global::Reqnroll.Table)(null)), "Then ");
#line hidden
            }
            await this.ScenarioCleanupAsync();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Search with an empty query")]
        [NUnit.Framework.CategoryAttribute("ui")]
        [NUnit.Framework.CategoryAttribute("api")]
        public async System.Threading.Tasks.Task SearchWithAnEmptyQuery()
        {
            string[] tagsOfScenario = new string[] {
                    "ui",
                    "api"};
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            global::Reqnroll.ScenarioInfo scenarioInfo = new global::Reqnroll.ScenarioInfo("Search with an empty query", null, tagsOfScenario, argumentsOfScenario, featureTags);
#line 25
    this.ScenarioInitialize(scenarioInfo);
#line hidden
            if ((global::Reqnroll.TagHelper.ContainsIgnoreTag(scenarioInfo.CombinedTags) || global::Reqnroll.TagHelper.ContainsIgnoreTag(featureTags)))
            {
                testRunner.SkipScenario();
            }
            else
            {
                await this.ScenarioStartAsync();
#line 26
    await testRunner.GivenAsync("Jeff is authenticated and authorised for searching products", ((string)(null)), ((global::Reqnroll.Table)(null)), "Given ");
#line hidden
#line 27
    await testRunner.WhenAsync("Jeff searches for \"\"", ((string)(null)), ((global::Reqnroll.Table)(null)), "When ");
#line hidden
#line 28
    await testRunner.ThenAsync("There should be items returned", ((string)(null)), ((global::Reqnroll.Table)(null)), "Then ");
#line hidden
            }
            await this.ScenarioCleanupAsync();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Scenario to be executed manually")]
        [NUnit.Framework.CategoryAttribute("manual")]
        public async System.Threading.Tasks.Task ScenarioToBeExecutedManually()
        {
            string[] tagsOfScenario = new string[] {
                    "manual"};
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            global::Reqnroll.ScenarioInfo scenarioInfo = new global::Reqnroll.ScenarioInfo("Scenario to be executed manually", null, tagsOfScenario, argumentsOfScenario, featureTags);
#line 34
    this.ScenarioInitialize(scenarioInfo);
#line hidden
            if ((global::Reqnroll.TagHelper.ContainsIgnoreTag(scenarioInfo.CombinedTags) || global::Reqnroll.TagHelper.ContainsIgnoreTag(featureTags)))
            {
                testRunner.SkipScenario();
            }
            else
            {
                await this.ScenarioStartAsync();
#line 35
    await testRunner.GivenAsync("User is authenticated and authorised for searching products", ((string)(null)), ((global::Reqnroll.Table)(null)), "Given ");
#line hidden
#line 36
    await testRunner.WhenAsync("User searches for \"this is something to check manually\"", ((string)(null)), ((global::Reqnroll.Table)(null)), "When ");
#line hidden
#line 37
    await testRunner.ThenAsync("No items should be returned", ((string)(null)), ((global::Reqnroll.Table)(null)), "Then ");
#line hidden
            }
            await this.ScenarioCleanupAsync();
        }
    }
}
#pragma warning restore
#endregion