using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using SentimentMovie.DAL;
using SentimentMovie.Domain;
using SentimentMovie.Models;

namespace SentimentMovie.Controllers
{
    public class HomeController : Controller
    {
        private IMovieViewConverter _movieViewConverter;
        private ISentimentInfo _sentimentInfo;
        private IStatisticsInfo _statisticsInfo;
        public HomeController(IMovieViewConverter movieViewConverter, 
            ISentimentInfo sentimentInfo, 
            IStatisticsInfo statisticsInfo)
        {
            _movieViewConverter = movieViewConverter;
            _sentimentInfo = sentimentInfo;
            _statisticsInfo = statisticsInfo;
        }

        public ActionResult GetConcreteMovie()
        {
            return PartialView("_GetConcreteMovie");
        }

        public ActionResult SentimentInfo()
        {
            return PartialView("_SentimentInfo");
        }

        public ActionResult Footer()
        {
            return PartialView("_Footer");
        }

        public IActionResult UpdateMovieInfo([FromBody] MovieUpdateModel movie)
        {
            _movieViewConverter.UpdateMovie(movie);
            _movieViewConverter.UpdateSentiment(movie);
            return new EmptyResult();
        }

        public IActionResult Index()
        {
            var movies = _movieViewConverter.GetMovieMainView();
            var sentiments = _sentimentInfo.GetInfo();
            var statistic = _statisticsInfo.GetStatistic();

            (IEnumerable<MovieViewModel> movies, 
                IEnumerable<SentimentViewModel> sentimes,
                StatisticsViewModel statistic) view = (movies, sentiments, statistic);
            return View(view);
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}
