export default async function fetchIngredientList() {
    const response = await fetch("http://localhost:3000/api/Ingredients");

    if (!response.ok) {
        throw new Error("Błąd podczas pobierania listy składników");
        
    }
    return response.json();
}
