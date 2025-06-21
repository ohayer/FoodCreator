import Card from "./Card";
import { useState } from "react";
import DishFormInputs from "./DishFormInputs";
import DishFormIngredients from "./DishFormIngredients";
import IngredientFormInputs from "./IngredientFormInputs.jsx";


const DishForm = () => {
    const [formIngredients, setFormIngredients] = useState([]);
    const [removedIngredient, setRemovedIngredient] = useState(null);

    const removeIngredientFromForm = (ingredient) => {
        setRemovedIngredient(null);
        setFormIngredients((prev) =>
            prev.filter((i) => i.IngredientId !== ingredient.IngredientId)
        );
        setRemovedIngredient(ingredient);
    };

    const handleIngredientAdd = (ingredient) => {
        ingredient.Quantity = 1;
        setFormIngredients([...formIngredients, ingredient]);
    };

    const increaseIngredientQuantity = (ingredient) => {
        setFormIngredients((prevIngredients) =>
            prevIngredients.map((i) =>
                i.IngredientId === ingredient.IngredientId
                    ? { ...i, Quantity: i.Quantity + 1 }
                    : i
            )
        );
    };

    const decreaseIngredientQuantity = (ingredient) => {
        setFormIngredients((prevIngredients) =>
            prevIngredients.map((i) =>
                i.IngridientId === ingredient.IngridientId && i.Quantity > 0
                    ? { ...i, Quantity: i.Quantity - 1 }
                    : i
            )
        );
    };
    return (
        <div className="flex h-screen">
            <div className="w-1/2 items-start justify-start">
                <h4 className="text-6xl mb-14 pt-16 pl-16">Stwórz własne danie:</h4>
                <div className="flex">
                    <DishFormInputs formIngredients={formIngredients} />
                    <div className="flex flex-col w-1/2">
                        <h4 className="pl-4 text-2xl mb-6">Składniki</h4>
                        <DishFormIngredients
                            formIngredients={formIngredients}
                            actions={{
                                increaseIngredientQuantity,
                                decreaseIngredientQuantity,
                                removeIngredientFromForm,
                            }}
                        />
                    </div>


                </div>
                <div className="flex h-screen">
                    <div className="w-1/2 items-start justify-start">
                        <IngredientFormInputs formIngredients={formIngredients} />

                    </div>

                </div>
            </div>


            <div className="w-1/2 h-full">
                <Card
                    onIngredientAdd={handleIngredientAdd}
                    removedIngredientFromForm={removedIngredient}
                />
            </div>
        </div>
    );
};
export default DishForm;
