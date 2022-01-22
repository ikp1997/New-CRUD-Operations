namespace CRUD_Operations
{
    public class Repository:IRepository
    {
        private readonly AppDbContext _dbContext;
        public Repository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public bool addCardModel(CardModel cardModelObject,IFormFile file)
        {
            try
            {
                var path = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).ToString()) + "\\Images";
                if (!System.IO.Directory.Exists(path))
                {
                    System.IO.Directory.CreateDirectory(path);
                }
                using (FileStream filestream = File.Create(path + "//" + cardModelObject.card_Name))
                {
                    file.CopyTo(filestream);
                }
                var type = file.ContentType.Remove(0,6);
                cardModelObject.imagePath = path + "\\" + cardModelObject.card_Name;
                var insertResult = _dbContext.CardTable.Add(cardModelObject);
                _dbContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
