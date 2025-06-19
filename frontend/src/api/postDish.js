export default async function postDish(dishData) {
    const response = await fetch("https://localhost:44385/api/dishes", {
        method: "POST",
        headers: {
            "Content-Type": "application/json"
        },
        body: JSON.stringify(dishData)
    });

    if (!response.ok) {
        throw new Error("Błąd podczas dodawania dania");
    }

    return response.json();
}
