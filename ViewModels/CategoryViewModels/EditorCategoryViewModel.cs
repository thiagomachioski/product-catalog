using Flunt.Notifications;
using Flunt.Validations;

namespace Products.Catalog.ViewModels.CategoryViewModels
{
    public class EditorCategoryViewModel : Notifiable, IValidatable
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public void Validate()
        {
            AddNotifications (
                new Contract()
                .IsNotNull(Title, "Title", "O titulo não pode ser nulo")
                .HasMinLen(Title, 3, "Title", "O titulo deve conter pelo menos 3 caracteres")
                .HasMaxLen(Title, 120, "Title", "O titulo deve conter até 120 caracteres")
            );
        }
    }
}
