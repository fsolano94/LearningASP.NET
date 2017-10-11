using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication8.Controllers
{
    public class WebPage
    {
        public WebPage(int id = 1, string url="N/A", string contentFormat="N/A", string content="N/A")
        {
            ID = id;
            _url = url;
            _contentFormat = contentFormat;
            _content = content;
        }
        public int ID { get; set; }
        public string _url { get; set; }
        public string _contentFormat { get; set; }
        public string _content { get; set; }
    }
    [Route("api/Values")]
    public class ValuesController : Controller
    {

        private static List<WebPage> _webPages = new List<WebPage>()
        {
            new WebPage(1,"http://my.io", "JSON", "cryptocurrency"),
            new WebPage(2, "http://yahoo.com", "RAW", "currency")
        };

        // GET api/values
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_webPages);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var result = _webPages.SingleOrDefault
                (
                    _webPages => _webPages.ID == id
                );
            if( result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]WebPage value)
        {
            _webPages.Add(value);
        }

        // PUT api/values/
        [HttpPut]
        public IActionResult Put([FromBody]WebPage value)
        {
            foreach (var webPage in _webPages)
            {
                if ( webPage.ID == value.ID )
                {
                    return BadRequest();
                }
            }

            _webPages.Add(value);

            return Ok(value);
        }

        // PUT api/values/
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]WebPage value)
        {
            for (int i = 0; i < _webPages.Count; i++)
            {
                if (_webPages[i].ID == id )
                {
                    _webPages[i] = value;
                    return Ok(value);
                }
            }
            return NotFound();
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            foreach (var webPage in _webPages)
            {
                if ( webPage.ID == id )
                {
                    _webPages.Remove(webPage);
                    return Ok();
                }
            }
            return NotFound();
        }
    }
}
