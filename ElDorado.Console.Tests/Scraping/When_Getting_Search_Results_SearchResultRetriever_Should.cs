﻿using ElDorado.Console.Trello;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telerik.JustMock;
using Telerik.JustMock.Helpers;

namespace ElDorado.Console.Tests.Scraping
{
    [TestClass]
    public class When_Getting_Search_Results_SearchResultRetriever_Should
    {
        private const string SearchString = "strawberry";
        private const string SearchResult = "{\"kind\":\"customsearch#search\",\"url\":{\"type\":\"application/json\",\"template\":\"https://www.googleapis.com/customsearch/v1?q={searchTerms}&num={count?}&start={startIndex?}&lr={language?}&safe={safe?}&cx={cx?}&sort={sort?}&filter={filter?}&gl={gl?}&cr={cr?}&googlehost={googleHost?}&c2coff={disableCnTwTranslation?}&hq={hq?}&hl={hl?}&siteSearch={siteSearch?}&siteSearchFilter={siteSearchFilter?}&exactTerms={exactTerms?}&excludeTerms={excludeTerms?}&linkSite={linkSite?}&orTerms={orTerms?}&relatedSite={relatedSite?}&dateRestrict={dateRestrict?}&lowRange={lowRange?}&highRange={highRange?}&searchType={searchType}&fileType={fileType?}&rights={rights?}&imgSize={imgSize?}&imgType={imgType?}&imgColorType={imgColorType?}&imgDominantColor={imgDominantColor?}&alt=json\"},\"queries\":{\"request\":[{\"title\":\"GoogleCustomSearch-strawberries\",\"totalResults\":\"265000000\",\"searchTerms\":\"strawberries\",\"count\":10,\"startIndex\":1,\"inputEncoding\":\"utf8\",\"outputEncoding\":\"utf8\",\"safe\":\"off\",\"cx\":\"asdf\"}],\"nextPage\":[{\"title\":\"GoogleCustomSearch-strawberries\",\"totalResults\":\"265000000\",\"searchTerms\":\"strawberries\",\"count\":10,\"startIndex\":11,\"inputEncoding\":\"utf8\",\"outputEncoding\":\"utf8\",\"safe\":\"off\",\"cx\":\"005715214518926163004:txoiceucevk\"}]},\"context\":{\"title\":\"ElDorado\"},\"searchInformation\":{\"searchTime\":0.294503,\"formattedSearchTime\":\"0.29\",\"totalResults\":\"265000000\",\"formattedTotalResults\":\"265,000,000\"},\"items\":[{\"kind\":\"customsearch#result\",\"title\":\"Strawberry-Wikipedia\",\"htmlTitle\":\"\u003cb\u003eStrawberry\u003c/b\u003e-Wikipedia\",\"link\":\"https://en.wikipedia.org/wiki/Strawberry\",\"displayLink\":\"en.wikipedia.org\",\"snippet\":\"ThegardenstrawberryisawidelygrownhybridspeciesofthegenusFragaria,\ncollectivelyknownasthestrawberries.Itiscultivatedworldwideforitsfruit.\",\"htmlSnippet\":\"Thegarden\u003cb\u003estrawberry\u003c/b\u003eisawidelygrownhybridspeciesofthegenusFragaria,\u003cbr\u003e\ncollectivelyknownasthe\u003cb\u003estrawberries\u003c/b\u003e.Itiscultivatedworldwideforitsfruit.\",\"cacheId\":\"bkPI4CPGXdcJ\",\"formattedUrl\":\"https://en.wikipedia.org/wiki/Strawberry\",\"htmlFormattedUrl\":\"https://en.wikipedia.org/wiki/\u003cb\u003eStrawberry\u003c/b\u003e\",\"pagemap\":{\"cse_thumbnail\":[{\"width\":\"275\",\"height\":\"183\",\"src\":\"https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQwosluJuurc5n6c4c36PPbC5fpTl-ofYcVvClMzbcDx1K8e2rfM5MsdcRU\"}],\"metatags\":[{\"referrer\":\"origin\",\"og:image\":\"https://upload.wikimedia.org/wikipedia/commons/thumb/7/7e/Strawberry_BNC.jpg/1200px-Strawberry_BNC.jpg\"}],\"cse_image\":[{\"src\":\"https://upload.wikimedia.org/wikipedia/commons/thumb/7/7e/Strawberry_BNC.jpg/1200px-Strawberry_BNC.jpg\"}]}},{\"kind\":\"customsearch#result\",\"title\":\"Strawberries:Benefits,nutrition,andrisks\",\"htmlTitle\":\"\u003cb\u003eStrawberries\u003c/b\u003e:Benefits,nutrition,andrisks\",\"link\":\"https://www.medicalnewstoday.com/articles/271285.php\",\"displayLink\":\"www.medicalnewstoday.com\",\"snippet\":\"Dec19,2017...Strawberriesrankamongthetop10fruitsandvegetablesintheirantioxidant\ncapacity.Theyareoneofthebestfruitstoconsumeandcan...\",\"htmlSnippet\":\"Dec19,2017\u003cb\u003e...\u003c/b\u003e\u003cb\u003eStrawberries\u003c/b\u003erankamongthetop10fruitsandvegetablesintheirantioxidant\u003cbr\u003e\ncapacity.Theyareoneofthebestfruitstoconsumeandcan&nbsp;...\",\"cacheId\":\"cPMjRiWCr9cJ\",\"formattedUrl\":\"https://www.medicalnewstoday.com/articles/271285.php\",\"htmlFormattedUrl\":\"https://www.medicalnewstoday.com/articles/271285.php\",\"pagemap\":{\"cse_thumbnail\":[{\"width\":\"256\",\"height\":\"192\",\"src\":\"https://encrypted-tbn1.gstatic.com/images?q=tbn:ANd9GcTJ3ql3j3muDo6zEN4_4o2gx7tFRC5sudXfgPg8rG8epumIFROSnAJbQ12k\"}],\"metatags\":[{\"og:url\":\"https://www.medicalnewstoday.com/articles/271285.php\",\"og:type\":\"article\",\"og:site_name\":\"MedicalNewsToday\",\"og:title\":\"Strawberries:Benefits,nutrition,andrisks\",\"og:description\":\"Strawberriesrankamongthetop10fruitsandvegetablesintheirantioxidantcapacity.Theyareoneofthebestfruitstoconsumeandcanbenefittheheartandcardiovascularhealth,aswellasaddingtoaperson'sdailyfruitandvegintake.Learnallaboutthehealthbenefitsofeatingstrawberrieshere.\",\"og:image\":\"https://cdn1.medicalnewstoday.com/content/images/headlines/271/271285/three-strawberries.jpg\",\"fb:admins\":\"1658973110\",\"fb:app_id\":\"147184211975697\",\"twitter:domain\":\"medicalnewstoday.com\",\"twitter:card\":\"summary_large_image\",\"twitter:site\":\"@mnt\",\"twitter:title\":\"Strawberries:Benefits,nutrition,andrisks\",\"twitter:description\":\"Strawberriesrankamongthetop10fruitsandvegetablesintheirantioxidantcapacity.Theyareoneofthebestfruitstoconsumeandcanbenefittheheartandcardiovascularhealth,aswellasaddingtoaperson'sdailyfruitandvegintake.Learnallaboutthehealthbenefitsofeatingstrawberrieshere.\",\"twitter:creator\":\"@mnt\",\"twitter:image:src\":\"https://cdn1.medicalnewstoday.com/content/images/headlines/271/271285/three-strawberries.jpg\",\"rating\":\"general\",\"p:domain_verify\":\"ca332f19ebf583ba72eff99cc210a982\",\"viewport\":\"width=device-width,initial-scale=1.0,minimum-scale=1.0,maximum-scale=1.0,user-scalable=0\"}],\"newsarticle\":[{\"headline\":\"Everythingyouneedtoknowaboutstrawberries\",\"datemodified\":\"2017-12-19\",\"articlebody\":\"TableofcontentsBenefitsNutritionDietRisksandprecautionsFreshsummerstrawberriesareoneofthemostpopular,refreshing,andhealthytreatsontheplanet.Today,thereareover600...\",\"genre\":\"Nutrition/Diet\",\"url\":\"https://www.medicalnewstoday.com/articles/271285.php\"}],\"cse_image\":[{\"src\":\"https://cdn1.medicalnewstoday.com/content/images/headlines/271/271285/three-strawberries.jpg\"}]}},{\"kind\":\"customsearch#result\",\"title\":\"Thehealthbenefitsofstrawberries|BBCGoodFood\",\"htmlTitle\":\"Thehealthbenefitsof\u003cb\u003estrawberries\u003c/b\u003e|BBCGoodFood\",\"link\":\"https://www.bbcgoodfood.com/howto/guide/ingredient-focus-strawberries\",\"displayLink\":\"www.bbcgoodfood.com\",\"snippet\":\"It'sdifficulttoresistaperfectlyripestrawberry,butdidyouknowjusthowgood\ntheyareforyou?NutritionistJoLewinsharesthenutritionbenefitsofthebright\nred...\",\"htmlSnippet\":\"It&#39;sdifficulttoresistaperfectlyripe\u003cb\u003estrawberry\u003c/b\u003e,butdidyouknowjusthowgood\u003cbr\u003e\ntheyareforyou?NutritionistJoLewinsharesthenutritionbenefitsofthebright\u003cbr\u003e\nred&nbsp;...\",\"cacheId\":\"lfiwARoyPU4J\",\"formattedUrl\":\"https://www.bbcgoodfood.com/howto/.../ingredient-focus-strawberries\",\"htmlFormattedUrl\":\"https://www.bbcgoodfood.com/howto/.../ingredient-focus-\u003cb\u003estrawberries\u003c/b\u003e\",\"pagemap\":{\"cse_thumbnail\":[{\"width\":\"200\",\"height\":\"200\",\"src\":\"https://encrypted-tbn1.gstatic.com/images?q=tbn:ANd9GcRbesnccTtzBTDDwLuph88a3GxrBy3DZrMarlth0HyWNoTArgCFqmyi7b2y\"}],\"metatags\":[{\"p:domain_verify\":\"e0dc3ea18b52d6be8f7d588b29af3605\",\"viewport\":\"width=device-width,initial-scale=1.0,user-scalable=no\",\"fb:pages\":\"31296838546\",\"og:site_name\":\"BBCGoodFood\",\"og:type\":\"article\",\"og:url\":\"https://www.bbcgoodfood.com/howto/guide/ingredient-focus-strawberries\",\"og:title\":\"Thehealthbenefitsofstrawberries\",\"og:description\":\"It'sdifficulttoresistaperfectlyripestrawberry,butdidyouknowjusthowgoodtheyareforyou?NutritionistJoLewinsharesthenutritionbenefitsofthebrightredberry.\",\"og:image\":\"https://www.bbcgoodfood.com/sites/default/files/guide/hub-image/2013/06/health-benefits-strawberries-hub-image-200-200.jpg\",\"twitter:card\":\"summary\",\"twitter:url\":\"https://www.bbcgoodfood.com/howto/guide/ingredient-focus-strawberries\",\"twitter:title\":\"Thehealthbenefitsofstrawberries\",\"twitter:description\":\"It'sdifficulttoresistaperfectlyripestrawberry,butdidyouknowjusthowgoodtheyareforyou?NutritionistJoLewinsharesthenutritionbenefitsofthebrightredberry.\",\"apple-mobile-web-app-title\":\"GoodFood\",\"msapplication-tileimage\":\"/sites/all/themes/bbcw_goodfood/bbcgf2015touch-114x114.png\",\"msapplication-tilecolor\":\"#ffffff\"}],\"cse_image\":[{\"src\":\"https://www.bbcgoodfood.com/sites/default/files/guide/hub-image/2013/06/health-benefits-strawberries-hub-image-200-200.jpg\"}]}},{\"kind\":\"customsearch#result\",\"title\":\"Strawberries:Planting,GrowingandHarvestingStrawberryPlants...\",\"htmlTitle\":\"\u003cb\u003eStrawberries\u003c/b\u003e:Planting,GrowingandHarvesting\u003cb\u003eStrawberry\u003c/b\u003ePlants...\",\"link\":\"https://www.almanac.com/plant/strawberries\",\"displayLink\":\"www.almanac.com\",\"snippet\":\"Learnhowtoplant,grow,andharveststrawberrieswiththisgrowingguidefrom\nTheOldFarmer'sAlmanac.\",\"htmlSnippet\":\"Learnhowtoplant,grow,andharvest\u003cb\u003estrawberries\u003c/b\u003ewiththisgrowingguidefrom\u003cbr\u003e\nTheOldFarmer&#39;sAlmanac.\",\"cacheId\":\"5zxaKLmL0KoJ\",\"formattedUrl\":\"https://www.almanac.com/plant/strawberries\",\"htmlFormattedUrl\":\"https://www.almanac.com/plant/\u003cb\u003estrawberries\u003c/b\u003e\",\"pagemap\":{\"cse_thumbnail\":[{\"width\":\"275\",\"height\":\"183\",\"src\":\"https://encrypted-tbn1.gstatic.com/images?q=tbn:ANd9GcSPy5yClEdrBD4TT3QRtjnOkl4E5zASLCwNsz8FCzkGtLKi5iQ6zyUIv0mS\"}],\"Post\":[{\"title\":\"AboutStrawberries\",\"date\":\"2018-09-13T05:05:39-04:00\",\"created\":\"2018-09-13T05:05:39-04:00\",\"encoded\":\"Whyisthestrawberryfruitsweet?Whydoesthestrawberryflowerbecomethestrawberryfruit?Ifyoucouldanswerthesequestions,thatwouldbeamazing.\"},{\"title\":\"mulchingstrawberries\",\"date\":\"2018-09-05T15:02:46-04:00\",\"created\":\"2018-09-05T15:02:46-04:00\",\"encoded\":\"CanIusegrasscuttingstomulchplants?Doalltheplantsgetcutdown?Ihesitatetodothat!\"},{\"title\":\"MulchingStrawberries\",\"date\":\"2018-09-06T16:53:37-04:00\",\"created\":\"2018-09-06T16:53:37-04:00\",\"encoded\":\"Yes,youcanusegrassclippingstomulchstrawberries,aslongasthegrasscomesfromapesticide-freeareaandtherearenoweeds’seedsmixedin.Strawisabetteralternative,however....\"},{\"title\":\"notsureaboutstrawberries\",\"date\":\"2019-01-01T09:25:37-05:00\",\"created\":\"2019-01-01T09:25:37-05:00\",\"encoded\":\"Areyousurethatitwillbesuccesinthisway\"},{\"title\":\"ContainerStrawberryPlants,AdvicePlease.\",\"date\":\"2018-08-14T23:46:09-04:00\",\"created\":\"2018-08-14T23:46:09-04:00\",\"encoded\":\"Hi,Firstyearofgrowingcontainerstrawberryplants.They'vedoneextremelywell,largerobustplants.DoIbringtheminsideoverthewinter?HowtoIproceedafterthegrowingseasonis...\"},{\"title\":\"Strawberriesareoneofthoselittlepleasuresinlife.\",\"date\":\"2018-08-01T09:41:25-04:00\",\"created\":\"2018-08-01T09:41:25-04:00\",\"encoded\":\"LaxmiStrawberryFarm,theplacetobeallyearroundandespeciallyatstrawberryseason!!!Comeandspendthedaypickingandeatingripeorganicstrawberries,downinourfields!\"},{\"title\":\"Howlongbeforegerminatedstrawberriescanbeoutside?\",\"date\":\"2018-07-09T02:24:13-04:00\",\"created\":\"2018-07-09T02:24:13-04:00\",\"encoded\":\"Icurrentlyliveinatownhousewithroomforatinygardenintheback.Theproblemisthatitisonthenorthsideandisprettyshadymostoftheday.ThefirstyearIwashere,therewas...\"},{\"title\":\"whentoplantstrawberries\",\"date\":\"2018-07-12T11:04:38-04:00\",\"created\":\"2018-07-12T11:04:38-04:00\",\"encoded\":\"Youareambitious!Toughquestionprobablybecausetherearevariablesrespecificconditions(light,soil,moisture,etc)and,perhaps,varieties.Wefoundrecommendationsforstartingthe...\"},{\"title\":\"getting2-3yearoldstrawberriesundercontrol\",\"date\":\"2018-07-08T13:11:55-04:00\",\"created\":\"2018-07-08T13:11:55-04:00\",\"encoded\":\"Iplantedafewstrawberryplants,notsurewhatvariety,2-3yearsago,andhaveprettymuchleftthemalonebecauseunsurehowtocareforthem.Theyhavespreadlikecrazy,andaremixed...\"}],\"metatags\":[{\"viewport\":\"width=device-width,initial-scale=1.0,user-scalable=yes\",\"mobileoptimized\":\"width\",\"handheldfriendly\":\"true\",\"apple-mobile-web-app-capable\":\"yes\",\"rights\":\"©2018,YankeePublishingInc\",\"fb:admins\":\"100002198720176\",\"fb:app_id\":\"180003315384377\",\"og:site_name\":\"OldFarmer'sAlmanac\",\"og:type\":\"article\",\"og:title\":\"Strawberries\",\"og:url\":\"https://www.almanac.com/plant/strawberries\",\"og:description\":\"Learnhowtoplant,grow,andharveststrawberrieswiththisgrowingguidefromTheOldFarmer'sAlmanac.\",\"og:image\":\"https://www.almanac.com/sites/default/files/image_nodes/strawberries-1.jpg\",\"twitter:card\":\"summary_large_image\",\"twitter:site\":\"@almanac\",\"twitter:creator\":\"@almanac\",\"twitter:site:id\":\"9252072\",\"twitter:creator:id\":\"9252072\",\"twitter:title\":\"Strawberries\",\"twitter:description\":\"Learnhowtoplant,grow,andharveststrawberrieswiththisgrowingguidefromTheOldFarmer'sAlmanac.\",\"twitter:url\":\"https://www.almanac.com/plant/strawberries\",\"twitter:image\":\"https://www.almanac.com/sites/default/files/image_nodes/strawberries-1.jpg\",\"article:tag\":\"--Fruit\",\"dcterms.title\":\"Strawberries\",\"dcterms.creator\":\"OldFarmer'sAlmanac\",\"dcterms.rights\":\"(c)2018,YankeePublishingInc\",\"theme-color\":\"#CC0000\",\"msapplication-navbutton-color\":\"#CC0000\",\"apple-mobile-web-app-status-bar-style\":\"#CC0000\",\"schema:ratingvalue\":\"4\",\"schema:ratingcount\":\"784\"}],\"cse_image\":[{\"src\":\"https://www.almanac.com/sites/default/files/image_nodes/strawberries-1.jpg\"}]}},{\"kind\":\"customsearch#result\",\"title\":\"FloridaStrawberryFestival–11-dayeventcelebratingthe...\",\"htmlTitle\":\"Florida\u003cb\u003eStrawberry\u003c/b\u003eFestival–11-dayeventcelebratingthe...\",\"link\":\"https://flstrawberryfestival.com/\",\"displayLink\":\"flstrawberryfestival.com\",\"snippet\":\"TheFloridaStrawberryFestivalisan11-daycommunityeventcelebratingthe\nstrawberryharvestofEasternHillsboroughCounty.Eachyear,over500,000...\",\"htmlSnippet\":\"TheFlorida\u003cb\u003eStrawberry\u003c/b\u003eFestivalisan11-daycommunityeventcelebratingthe\u003cbr\u003e\n\u003cb\u003estrawberry\u003c/b\u003eharvestofEasternHillsboroughCounty.Eachyear,over500,000&nbsp;...\",\"cacheId\":\"kWb7cXTNY3cJ\",\"formattedUrl\":\"https://flstrawberryfestival.com/\",\"htmlFormattedUrl\":\"https://fl\u003cb\u003estrawberry\u003c/b\u003efestival.com/\",\"pagemap\":{\"cse_thumbnail\":[{\"width\":\"300\",\"height\":\"168\",\"src\":\"https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcSJ_1zuWY-YVb7Hsno80hh9XUTx0bc9KD1JW8Sc2vk9LCbEnzyHmRwlDJre\"}],\"metatags\":[{\"format-detection\":\"telephone=no\",\"viewport\":\"width=device-width,initial-scale=1,user-scalable=no\",\"msapplication-tileimage\":\"https://flstrawberryfestival.com/wp-content/uploads/2017/10/cropped-food-and-consessions-270x270.jpg\"}],\"cse_image\":[{\"src\":\"https://flstrawberryfestival.com/wp-content/uploads/2016/10/events-1.jpg\"}]}},{\"kind\":\"customsearch#result\",\"title\":\"Strawberries\",\"htmlTitle\":\"\u003cb\u003eStrawberries\u003c/b\u003e\",\"link\":\"http://www.whfoods.com/genpage.php?tname=foodspice&dbid=32\",\"displayLink\":\"www.whfoods.com\",\"snippet\":\"Recentstudieshaveexaminedthetotalantioxidantcapacity(TAC)of\nstrawberries,notonlyincomparisonwithotherfruits,butalsoincomparisonwith\nfoodsin...\",\"htmlSnippet\":\"Recentstudieshaveexaminedthetotalantioxidantcapacity(TAC)of\u003cbr\u003e\n\u003cb\u003estrawberries\u003c/b\u003e,notonlyincomparisonwithotherfruits,butalsoincomparisonwith\u003cbr\u003e\nfoodsin&nbsp;...\",\"cacheId\":\"OV5HU9PMcDYJ\",\"formattedUrl\":\"www.whfoods.com/genpage.php?tname=foodspice&dbid=32\",\"htmlFormattedUrl\":\"www.whfoods.com/genpage.php?tname=foodspice&amp;dbid=32\",\"pagemap\":{\"metatags\":[{\"viewport\":\"width=device-width,initial-scale=1.0,maximum-scale=1.0,user-scalable=no\"}]}},{\"kind\":\"customsearch#result\",\"title\":\"EWG's2018Shopper'sGuidetoPesticidesinProduce|Strawberries\",\"htmlTitle\":\"EWG&#39;s2018Shopper&#39;sGuidetoPesticidesinProduce|\u003cb\u003eStrawberries\u003c/b\u003e\",\"link\":\"https://www.ewg.org/foodnews/strawberries.php\",\"displayLink\":\"www.ewg.org\",\"snippet\":\"TheaverageAmericaneatsabouteightpoundsoffreshstrawberriesayear–\nandwiththemdozensofpesticides.\",\"htmlSnippet\":\"TheaverageAmericaneatsabouteightpoundsoffresh\u003cb\u003estrawberries\u003c/b\u003eayear–\u003cbr\u003e\nandwiththemdozensofpesticides.\",\"cacheId\":\"2tfNZVIrRaoJ\",\"formattedUrl\":\"https://www.ewg.org/foodnews/strawberries.php\",\"htmlFormattedUrl\":\"https://www.ewg.org/foodnews/\u003cb\u003estrawberries\u003c/b\u003e.php\",\"pagemap\":{\"cse_thumbnail\":[{\"width\":\"310\",\"height\":\"163\",\"src\":\"https://encrypted-tbn2.gstatic.com/images?q=tbn:ANd9GcRQRz-xoSQyKnqHB6hKpqnyJhl00tgG1sFDuwHEbTTH__rEG-Lel6ugp7c\"}],\"metatags\":[{\"author\":\"EnvironmentalWorkingGroup\",\"viewport\":\"width=device-width,initial-scale=1.0,minimum-scale=1.0,maximum-scale=1.0,user-scalable=no\",\"og:url\":\"https://www.ewg.org/foodnews/strawberries.php\",\"og:title\":\"Pesticides+PoisonGases=Cheap,Year-RoundStrawberries\",\"og:description\":\"TheaverageAmericaneatsabouteightpoundsoffreshstrawberriesayear–andwiththemdozensofpesticides.\",\"og:image\":\"https://static.ewg.org/reports/2018/foodnews/img/EWG_Social_Share_FN-Strawberry_C01.jpg\",\"og:type\":\"website\",\"twitter:card\":\"summary\",\"twitter:site\":\"@ewg\",\"twitter:title\":\"Pesticides+PoisonGases=Cheap,Year-RoundStrawberries#DirtyDozen|@ewg|\"}],\"cse_image\":[{\"src\":\"https://static.ewg.org/reports/2018/foodnews/img/EWG_Social_Share_FN-Strawberry_C01.jpg\"}]}},{\"kind\":\"customsearch#result\",\"title\":\"Strawberries&OrganicStrawberries|Driscoll's\",\"htmlTitle\":\"\u003cb\u003eStrawberries\u003c/b\u003e&amp;Organic\u003cb\u003eStrawberries\u003c/b\u003e|Driscoll&#39;s\",\"link\":\"https://www.driscolls.com/berries/strawberries\",\"displayLink\":\"www.driscolls.com\",\"snippet\":\"Formorethan100years,theDriscoll'snamehasmeantfreshanddelicious\nstrawberries.Foranimpromptucelebrationyourfriendswillreminisceaboutfor...\",\"htmlSnippet\":\"Formorethan100years,theDriscoll&#39;snamehasmeantfreshanddelicious\u003cbr\u003e\n\u003cb\u003estrawberries\u003c/b\u003e.Foranimpromptucelebrationyourfriendswillreminisceaboutfor&nbsp;...\",\"formattedUrl\":\"https://www.driscolls.com/berries/strawberries\",\"htmlFormattedUrl\":\"https://www.driscolls.com/berries/\u003cb\u003estrawberries\u003c/b\u003e\",\"pagemap\":{\"cse_thumbnail\":[{\"width\":\"275\",\"height\":\"183\",\"src\":\"https://encrypted-tbn3.gstatic.com/images?q=tbn:ANd9GcSgKpMKP2-YxcfvcVw-5Jw-6x2-YXCQ-R1dyEed7iouWKmcS8EIzpJ6Iyk\"}],\"metatags\":[{\"viewport\":\"width=device-width,initial-scale=1\",\"og:title\":\"Strawberries&OrganicStrawberries|Driscoll's\",\"og:type\":\"website\",\"og:url\":\"https://www.driscolls.com/berries/strawberries\",\"og:image\":\"https://driscolls.imgix.net/-/media/images/pages/berries/strawberry/pim1straw-on-wood-1508570101.ashx\",\"og:site_name\":\"Driscoll's\",\"og:description\":\"Formorethan100years,theDriscoll’snamehasmeantfreshanddeliciousstrawberries.Foranimpromptucelebrationyourfriendswillreminisceaboutforyearstocome,bringoutourfreshandalwaysdeliciousstrawberriestomakememoriesyouwon'tforget!\",\"twitter:card\":\"summary\",\"twitter:url\":\"https://www.driscolls.com/berries/strawberries\",\"twitter:title\":\"Strawberries&OrganicStrawberries|Driscoll's\",\"twitter:description\":\"Formorethan100years,theDriscoll’snamehasmeantfreshanddeliciousstrawberries.Foranimpromptucelebrationyourfriendswillreminisceaboutforyearstocome,bringoutourfreshandalwaysdeliciousstrawberriestomakememoriesyouwon'tforget!\",\"twitter:image\":\"https://driscolls.imgix.net/-/media/images/pages/berries/strawberry/pim1straw-on-wood-1508570101.ashx\",\"itemprop:url\":\"https://www.driscolls.com/berries/strawberries\",\"fb:app_id\":\"1964575563768735\"}],\"cse_image\":[{\"src\":\"https://driscolls.imgix.net/-/media/images/pages/berries/strawberry/pim1straw-on-wood-1508570101.ashx\"}]}},{\"kind\":\"customsearch#result\",\"title\":\"NutritionalBenefitsoftheStrawberry\",\"htmlTitle\":\"NutritionalBenefitsofthe\u003cb\u003eStrawberry\u003c/b\u003e\",\"link\":\"https://www.webmd.com/diet/features/nutritional-benefits-of-the-strawberry\",\"displayLink\":\"www.webmd.com\",\"snippet\":\"Theheart-shapedsilhouetteofthestrawberryisthefirstcluethatthisfruitisgood\nforyou.Thesepotentlittlepackagesprotectyourheart,increaseHDL(good)...\",\"htmlSnippet\":\"Theheart-shapedsilhouetteofthe\u003cb\u003estrawberry\u003c/b\u003eisthefirstcluethatthisfruitisgood\u003cbr\u003e\nforyou.Thesepotentlittlepackagesprotectyourheart,increaseHDL(good)&nbsp;...\",\"cacheId\":\"cVcBInDPw1cJ\",\"formattedUrl\":\"https://www.webmd.com/diet/.../nutritional-benefits-of-the-strawberry\",\"htmlFormattedUrl\":\"https://www.webmd.com/diet/.../nutritional-benefits-of-the-\u003cb\u003estrawberry\u003c/b\u003e\",\"pagemap\":{\"cse_thumbnail\":[{\"width\":\"200\",\"height\":\"200\",\"src\":\"https://encrypted-tbn3.gstatic.com/images?q=tbn:ANd9GcQl2YDG3KsEH8S4STzHnOzgDXxYFWIf7gBermk1PogGpEArMcrcpekkLGY\"}],\"metatags\":[{\"viewport\":\"width=device-width,initial-scale=1\",\"fb:pages\":\"11736558481\",\"og:title\":\"NutritionalBenefitsoftheStrawberry\",\"og:description\":\"ThetinystrawberryispackedwithvitaminC,fiber,antioxidants,andmore.\",\"og:image\":\"https://img.webmd.com/dtmcms/live/webmd/consumer_assets/site_images/logos/webmd/web/webmd-logo-fb.jpg\",\"og:url\":\"https://www.webmd.com/diet/features/nutritional-benefits-of-the-strawberry\",\"og:site_name\":\"WebMD\",\"og:type\":\"article\",\"og:locale\":\"en_US\",\"fb:app_id\":\"385978254785998\",\"article:author\":\"https://www.facebook.com/WebMD\",\"article:publisher\":\"https://www.facebook.com/WebMD\",\"s_1stimp\":\"1637\"}],\"sitenavigationelement\":[{\"url\":\"ADD/ADHD\",\"name\":\"ADD/ADHD\"}],\"listitem\":[{\"item\":\"Diet&WeightManagement\",\"name\":\"Diet&WeightManagement\",\"position\":\"1\"},{\"item\":\"FeatureStories\",\"name\":\"FeatureStories\",\"position\":\"2\"}],\"cse_image\":[{\"src\":\"https://img.webmd.com/dtmcms/live/webmd/consumer_assets/site_images/logos/webmd/web/webmd-logo-fb.jpg\"}]}},{\"kind\":\"customsearch#result\",\"title\":\"CaliforniaStrawberryCommission:Home\",\"htmlTitle\":\"California\u003cb\u003eStrawberry\u003c/b\u003eCommission:Home\",\"link\":\"https://www.calstrawberry.com/en-us/\",\"displayLink\":\"www.calstrawberry.com\",\"snippet\":\"TheCaliforniaStrawberryCommissionisastategovernmentagencylocatedin\nNorthernCaliforniastategovernmentagencychargedwithconductingresearch...\",\"htmlSnippet\":\"TheCalifornia\u003cb\u003eStrawberry\u003c/b\u003eCommissionisastategovernmentagencylocatedin\u003cbr\u003e\nNorthernCaliforniastategovernmentagencychargedwithconductingresearch&nbsp;...\",\"cacheId\":\"mUiL6AG4sAEJ\",\"formattedUrl\":\"https://www.calstrawberry.com/en-us/\",\"htmlFormattedUrl\":\"https://www.cal\u003cb\u003estrawberry\u003c/b\u003e.com/en-us/\",\"pagemap\":{\"metatags\":[{\"viewport\":\"width=device-width,initial-scale=1,maximum-scale=1,user-scalable=no\"}]}}]}";

        private const string ApiKey = "asdf";
        private const string CustomSearchEngineId = "contains::colon";

        private SimpleWebClient Client = Mock.Create<SimpleWebClient>();

        private SearchResultRetriever Target { get; set; }

        [TestInitialize]
        public void BeforeEachTest()
        {
            Client.Arrange(cl => cl.GetRawResultOfBasicGetRequest(Arg.AnyString)).Returns(SearchResult);

            var store = new CredentialStore($"ApiKey:{ApiKey}\nCseId:{CustomSearchEngineId}");

            Target = new SearchResultRetriever(Client, store);
            Target.Delay = i => { };
        }

        [TestMethod, Owner("ebd"), TestCategory("Proven"), TestCategory("Unit")]
        public void Return_A_Collection_Of_SearchResults()
        {
            Client.Arrange(cl => cl.GetRawResultOfBasicGetRequest(Arg.AnyString)).Returns(string.Empty);

            var results = Target.SearchFor(SearchString);

            results.ShouldBeEmpty();
        }

        [TestMethod, Owner("ebd"), TestCategory("Proven"), TestCategory("Unit")]
        public void Return_A_Collection_With_Results_When_There_Are_Results()
        {
            var results = Target.SearchFor(SearchString);

            results.Count().ShouldBe(10);
        }

        [TestMethod, Owner("ebd"), TestCategory("Proven"), TestCategory("Unit")]
        public void Return_A_Collection_With_First_Link_Wikipedia()
        {
            var results = Target.SearchFor(SearchString);

            var firstResult = results.First();

            firstResult.DisplayLink.ShouldBe("en.wikipedia.org");
        }

        [TestMethod, Owner("ebd"), TestCategory("Proven"), TestCategory("Unit")]
        public void Build_The_Search_Using_Passed_In_ApiKey_And_SearchEngine_Key()
        {
            var unescapedString = "contains:colon";

            Target.BaseSearchQuery.ShouldBe($"https://www.googleapis.com/customsearch/v1?key={ApiKey}&cx={unescapedString}&q=");
        }

        [TestMethod, Owner("ebd"), TestCategory("Proven"), TestCategory("Unit")]
        public void Invoke_The_Web_Client_Twice_When_2_Serps_Are_Requested()
        {
            Target.SearchFor(SearchString, 2);

            Client.Assert(cl => cl.GetRawResultOfBasicGetRequest(Arg.AnyString), Occurs.Exactly(2));
        }

        [TestMethod, Owner("ebd"), TestCategory("Proven"), TestCategory("Unit")]
        public void Invoke_The_Client_With_num_equals_11_As_Query_Parameter()
        {
            Target.SearchFor(SearchString, 2);

            Client.Assert(cl => cl.GetRawResultOfBasicGetRequest(Target.BaseSearchQuery + SearchString + "&start=11"), Occurs.Once());
        }

        [TestMethod, Owner("ebd"), TestCategory("Proven"), TestCategory("Unit")]
        public void Invoke_The_Delay_Once_For_Two_Serps_Totaling_30_Seconds()
        {
            int delaySeconds = 0;
            Target.Delay = i => delaySeconds += i;

            Target.SearchFor(SearchString, 2);

            delaySeconds.ShouldBe(30);
        }

        [TestMethod, Owner("ebd"), TestCategory("Proven"), TestCategory("Unit")]
        public void Return_Empty_Results_When_Search_Yields_No_Items()
        {
            Client.Arrange(cl => cl.GetRawResultOfBasicGetRequest(Arg.AnyString)).ReturnsMany(SearchResult, "{\"kind\":\"customsearch#search\",\"url\":{\"type\":\"application/json\",\"template\":\"https://www.googleapis.com/customsearch/v1?q={searchTerms}&num={count?}&start={startIndex?}&lr={language?}&safe={safe?}&cx={cx?}&sort={sort?}&filter={filter?}&gl={gl?}&cr={cr?}&googlehost={googleHost?}&c2coff={disableCnTwTranslation?}&hq={hq?}&hl={hl?}&siteSearch={siteSearch?}&siteSearchFilter={siteSearchFilter?}&exactTerms={exactTerms?}&excludeTerms={excludeTerms?}&linkSite={linkSite?}&orTerms={orTerms?}&relatedSite={relatedSite?}&dateRestrict={dateRestrict?}&lowRange={lowRange?}&highRange={highRange?}&searchType={searchType}&fileType={fileType?}&rights={rights?}&imgSize={imgSize?}&imgType={imgType?}&imgColorType={imgColorType?}&imgDominantColor={imgDominantColor?}&alt=json\"},\"queries\":{\"request\":[{\"title\":\"GoogleCustomSearch-intitle:applicationintitle:security\"guestpostby\"\",\"totalResults\":\"0\",\"searchTerms\":\"intitle:applicationintitle:security\"guestpostby\"\",\"count\":10,\"startIndex\":11,\"inputEncoding\":\"utf8\",\"outputEncoding\":\"utf8\",\"safe\":\"off\",\"cx\":\"005715214518926163004:txoiceucevk\"}]},\"searchInformation\":{\"searchTime\":0.072811,\"formattedSearchTime\":\"0.07\",\"totalResults\":\"0\",\"formattedTotalResults\":\"0\"}}");

            var results = Target.SearchFor(SearchString, 2);

            results.Count().ShouldBe(10);
        }
    }
}
