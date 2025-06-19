export default async function fetchIngredientList() {
    const response = await fetch("https://localhost:44385/api/Ingredients");

    if (!response.ok) {
        throw new Error("Błąd podczas pobierania listy składników");
        
    }
    return response.json();
}
