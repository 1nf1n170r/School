const charactersListElement = document.getElementById("characters");
const searchInput = document.getElementById("searchInput");
const prevBtn = document.getElementById("prevBtn");
const nextBtn = document.getElementById("nextBtn");
const pageInfo = document.getElementById("pageInfo");

let characters = [];

let filteredCharacters = [];

let currentPage = 1;
const itemsPerPage = 5; 

async function fetchCharacters() {
  let url = "https://swapi.py4e.com/api/people/";

  try {
    while (url) {
      const response = await fetch(url);
      const data = await response.json();

      characters = characters.concat(data.results);

      url = data.next;
    }

    filteredCharacters = characters;
    renderCharacters();
  } catch (error) {
    console.error("Failed to fetch characters:", error);
  }
}

function renderCharacters() {
  charactersListElement.innerHTML = "";

  const startIndex = (currentPage - 1) * itemsPerPage;
  const endIndex = startIndex + itemsPerPage;

  const pageCharacters = filteredCharacters.slice(startIndex, endIndex);

  pageCharacters.forEach((character) => {
    const li = document.createElement("li");
    li.textContent = `
          Name: ${character.name} | 
          Height: ${character.height} | 
          Birth year: ${character.birth_year} | 
          Sex: ${character.gender}
        `;
    charactersListElement.appendChild(li);
  });

  const totalPages = Math.ceil(filteredCharacters.length / itemsPerPage);
  pageInfo.textContent = `Page ${currentPage} of ${totalPages}`;

  prevBtn.disabled = currentPage === 1;
  nextBtn.disabled = currentPage === totalPages;
}

searchInput.addEventListener("input", () => {
  const searchQuery = searchInput.value.toLowerCase();
  filteredCharacters = characters.filter((c) =>
    c.name.toLowerCase().includes(searchQuery)
  );
  currentPage = 1; 
  renderCharacters();
});

prevBtn.addEventListener("click", () => {
  if (currentPage > 1) {
    currentPage--;
    renderCharacters();
  }
});

nextBtn.addEventListener("click", () => {
  const totalPages = Math.ceil(filteredCharacters.length / itemsPerPage);
  if (currentPage < totalPages) {
    currentPage++;
    renderCharacters();
  }
});

fetchCharacters();
