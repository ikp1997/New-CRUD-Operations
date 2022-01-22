namespace CRUD_Operations
{
    public interface IRepository
    {
        public bool addCardModel(CardModel cardModelObject,IFormFile file);
    }
}
