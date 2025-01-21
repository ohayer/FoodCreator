import UrlInput from "./UrlInput";
import Card from "./Card";
import { useState, useEffect } from "react";

const formInputs = [
  {
    label: "Nazwa",
    type: "text",
    className: "input input-primary",
  },
  {
    label: "Zdjęcie (url)",
    type: "url",
    className: "url-input",
  },
  {
    label: "Cena w zł",
    type: "number",
    className: "input input-secondary",
  },
];

const DishForm = () => {
  const [formIngredients, setFormIngredients] = useState([]);

  const removeIngredientFromForm = ({ ingredient }) => {
    setFormIngredients(formIngredients.filter((i) => i.id !== ingredient.id));
    return formIngredients;
  };

  const handleIngredientAdd = (ingredient) => {
    // Dodaje składnik do stanu rodzica
    ingredient.quantity = 1;
    setFormIngredients([...formIngredients, ingredient]);
  };

  const increaseIngredientQuantity = (ingredient) => {
    setFormIngredients((prevIngredients) =>
      prevIngredients.map((i) =>
        i.id === ingredient.id ? { ...i, quantity: i.quantity + 1 } : i
      )
    );
  };

  const decreaseIngredientQuantity = (ingredient) => {
    setFormIngredients((prevIngredients) =>
      prevIngredients.map((i) =>
        i.id === ingredient.id && i.quantity > 0
          ? { ...i, quantity: i.quantity - 1 }
          : i
      )
    );
  };
  return (
    <div className="flex h-screen">
      <div className="w-1/2 items-start justify-start">
        <h4 className="text-6xl mb-14 pt-16 pl-16">Stwórz własne danie:</h4>
        <div className="flex">
          <div className="inputs pt-8 w-1/2 pl-12">
            {formInputs.map((input, index) => (
              <div className="flex flex-col mb-6" key={index}>
                <label className="text-lg font-medium mb-2">
                  {input.label}
                </label>
                {input.type === "url" ? (
                  <UrlInput />
                ) : (
                  <input
                    type={input.type}
                    placeholder={input.label}
                    className={input.className}
                  />
                )}
              </div>
            ))}
            <button className="btn btn-active btn-accent text-xl mt-6">
              Stwórz danie
            </button>
          </div>
          <div className="flex flex-col w-1/2">
            <h4 className="pl-4 text-2xl mb-6">Składniki</h4>
            <>
              {formIngredients.map((ingredient) => (
                <div
                  className="grid grid-cols-3 gap-4 bg-gray-900 p-4 m-4 rounded-lg"
                  key={ingredient.id}
                >
                  <div className="flex flex-col">
                    <p>{ingredient.name}</p>
                    <p>{ingredient.price}zł/szt</p>
                  </div>
                  <div className="flex items-center space-x-2 text-sm">
                    <button
                      onClick={() => increaseIngredientQuantity(ingredient)}
                      className="px-2 py-1 bg-gray-400 rounded hover:bg-gray-300 text-black"
                    >
                      ⯅
                    </button>
                    <input
                      type="text"
                      value={ingredient.quantity}
                      readOnly
                      className="w-12 text-center border border-gray-300 rounded"
                    />

                    <button
                      onClick={() => decreaseIngredientQuantity(ingredient)}
                      className="px-2 py-1 bg-gray-400 rounded hover:bg-gray-300 text-black"
                    >
                      ⯆
                    </button>
                  </div>
                  <button
                    onClick={() => removeIngredientFromForm({ ingredient })}
                    className="bg-red-500 text-white rounded-xl p-2 ml-4 cursor-pointer scale-75"
                  >
                    🗑
                  </button>
                </div>
              ))}
            </>
          </div>
        </div>
      </div>
      <div className="w-1/2 h-full">
        <Card onIngredientAdd={handleIngredientAdd} />
      </div>
    </div>
  );
};
export default DishForm;
