import { useState, useEffect } from 'react'
import fetchIngredientList from './api/fetchIngredientList'

const Card = ({ onIngredientAdd, removedIngredientFromForm }) => {
  const [ingredients, setIngredients] = useState([])

  useEffect(() => {
    const fetchData = async () => {
    const raw = await fetchIngredientList();
    const data = raw.map(item => ({
      IngredientId: item.IngredientID,
      Name:         item.Name,
      Price:        item.Price,
      Url:          item.Url,
    }));
    setIngredients(data);
  };
  fetchData();
  }, [])

  useEffect(() => {
    if (removedIngredientFromForm) {
      setIngredients(prev => [...prev, removedIngredientFromForm])
    }
  }, [removedIngredientFromForm])

  const onAddIngredient = ({ ingredient }) => {
    setIngredients(prev =>
      prev.filter(i => i.IngredientId !== ingredient.IngredientId)
    )
    onIngredientAdd(ingredient)
  }

  return (
    <div className='pt-8'>
      <h4 className='text-4xl'>Dostępne składniki</h4>
      <div className='grid grid-cols-2 gap-4 p-3  max-h-208  overflow-y-auto'>
        {ingredients.map((ingredient, idx) => (
          <div
            className='card bg-gray-700 w-96 shadow-sm scale-85'
            key={ingredient.IngredientId ?? idx}
          >
            <figure>
              <img src={ingredient.Url} alt={ingredient.Name} />
            </figure>
            <div className='card-body w-full'>
              <h2 className='card-title'>{ingredient.Name}</h2>
              <p>Cena {ingredient.Price} zł</p>
              <div className='card-actions justify-end'>
                <button
                  className='btn btn-success'
                  onClick={() => onAddIngredient({ ingredient })}
                >
                  Dodaj
                </button>
              </div>
            </div>
          </div>
        ))}
      </div>
    </div>
  )
}
export default Card
