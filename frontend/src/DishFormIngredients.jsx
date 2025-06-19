const DishFormIngredients = ({ formIngredients, actions }) => {
  return (
    <>
      {formIngredients.map((ingredient) => (
        <div
          className="grid grid-cols-3 gap-4 bg-gray-900 p-4 m-4 rounded-lg"
          key={ingredient.IngredientId}
        >
          <div className="flex flex-col">
            <p>{ingredient.Name}</p>
            <p>{ingredient.Price}zł/szt</p>
          </div>
          <div className="flex items-center space-x-2 text-sm">
            <button
              onClick={() => actions.increaseIngredientQuantity(ingredient)}
              className="px-2 py-1 bg-gray-400 rounded hover:bg-gray-300 text-black"
            >
              ⯅
            </button>
            <input
              type="text"
              value={ingredient.Quantity}
              readOnly
              className="w-12 text-center border border-gray-300 rounded"
            />

            <button
              onClick={() => actions.decreaseIngredientQuantity(ingredient)}
              className="px-2 py-1 bg-gray-400 rounded hover:bg-gray-300 text-black"
            >
              ⯆
            </button>
          </div>
          <button
            onClick={() => actions.removeIngredientFromForm(ingredient)}
            className="bg-red-500 text-white rounded-xl p-2 ml-4 cursor-pointer scale-75"
          >
            🗑
          </button>
        </div>
      ))}
    </>
  );
};
export default DishFormIngredients;
