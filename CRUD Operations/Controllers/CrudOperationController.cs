using Microsoft.AspNetCore.Mvc;
using System.IO;

namespace CRUD_Operations.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CrudOperationController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly ILogger<CrudOperationController> _logger;
        private AppDbContext _dbContext;
        private IHttpContextAccessor _httpContext;
        private readonly IRepository _irepository;

        public CrudOperationController(ILogger<CrudOperationController> logger,AppDbContext dbContext,IHttpContextAccessor httpContext,IRepository irepository)
        {
            _logger = logger;
            _dbContext = dbContext;
            _httpContext = httpContext;
            _irepository= irepository;
        }

        [HttpGet]
        [Route("getCardDetails")]
        public List<CardViewModel> GetCardDetails()
        {
            //CardViewModel cardViewModel = new CardViewModel();
            List<CardViewModel> cardViewModelList=new List<CardViewModel>(100);
            var cardModelListData = _dbContext.CardTable.ToList();
            foreach(var CardViewModelObject in cardModelListData)
            {
               
                System.IO.FileStream file = new System.IO.FileStream(CardViewModelObject.imagePath,System.IO.FileMode.Open,System.IO.FileAccess.Read);
                System.IO.BinaryReader binaryReader=new System.IO.BinaryReader(file);
                long byteLength=new System.IO.FileInfo(file.Name).Length;
                CardViewModel cardViewModel = new CardViewModel()
                {
                    cardId = CardViewModelObject.cardId,
                    card_Name = CardViewModelObject.card_Name,
                    price = CardViewModelObject.price,    
                    image= Convert.ToBase64String(binaryReader.ReadBytes((Int32)byteLength))
                };
            
                cardViewModelList.Add(cardViewModel);
                file.Close();
            }
            return cardViewModelList;
        }

        [HttpPost]
        [Route("addCardDetails")]
        public Boolean AddCard(string cardName,int cardPrice)
        {
            IFormFile file = _httpContext.HttpContext.Request.Form.Files[0];
            try
            {
                CardModel CardModelObject = new CardModel()
                {
                    card_Name=cardName,
                    price=cardPrice
                };
                _irepository.addCardModel(CardModelObject, file);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        [HttpDelete]
        [Route("deleteCardDetails")]
        public Boolean RemoveCard(int id)
        {
            var foundCard = _dbContext.CardTable.Find(id);
            System.IO.File.Delete(foundCard.imagePath);
            _dbContext.CardTable.Remove(foundCard);
            _dbContext.SaveChanges();
            return true;
        }

        [HttpPatch]
        [Route("editCardDetails")]
        public Boolean EditCard(int id,string cardname,int price)
        {
            var foundCard = _dbContext.CardTable.Find(id);
            foundCard.card_Name = cardname;
            foundCard.price = price;
            _dbContext.SaveChanges();
            return true; 
        }

    }
}