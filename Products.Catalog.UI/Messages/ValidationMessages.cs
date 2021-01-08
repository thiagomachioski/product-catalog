namespace Products.Catalog.UI.Messages
{
    public static class ValidationMessages
    {
        #region Products

        public static string DescriptionNotNullMessage = "A descrição não pode ser nula";
        public static string DescriptionNotEmptyMessage = "A descrição não pode ser vazia";
        public static string DescriptionMinimumLengthMessage = "A descrição deve conter pelo menos 3 caracteres";
        public static string DescriptionMaxiumLengthMessage = "A descrição deve conter até 1024 caracteres";

        public static string TitleNotNullMessage = "O titulo não pode ser nulo";
        public static string TitleNotEmptyMessage = "O titulo não pode ser vazio";
        public static string TitleMinimumLengthMessage = "O titulo deve conter pelo menos 3 caracteres";
        public static string TitleMaxiumLengthMessage = "O titulo deve conter até 120 caracteres";

        public static string PriceNotNullMessage = "O preço não deve ser nulo";
        public static string PriceNotEmptyMessage = "O preço não deve ser vazio";
        public static string PriceGreatherThanMessage = "O preço deve ser maior que zero";

        public static string QuantityNotNullMessage = "A quantidade não deve ser nulo";
        public static string QuantityNotEmptyMessage = "A quantidade não deve ser vazia";
        public static string QuantityGreatherThanMessage = "O preço deve ser maior que zero";

        public static string CategoryIdNotNullMessage = "O id da categoria não deve ser nulo";
        public static string CategoryIdNotEmptyMessage = "O id da categoria não deve ser vazia";
        public static string CategoryIdGreatherThanMessage = "O id da categoria deve ser maior que zero";

        #endregion

    }
}