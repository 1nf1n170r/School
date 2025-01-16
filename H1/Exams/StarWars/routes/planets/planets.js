const planetList = document.getElementById("planetList");
const climateFilter = document.getElementById("climateFilter");

let planets = [];
let uniqueClimates = new Set();

async function fetchPlanets() {
  let url = "https://swapi.py4e.com/api/planets/";
  try {
    while (url) {
      const response = await fetch(url);
      const data = await response.json();
      planets = planets.concat(data.results);
      url = data.next;
    }
  } catch (error) {
    console.error("Error fetching planets:", error);
  }
}

function populateClimates() {
  planets.forEach((planet) => {
    const climates = planet.climate.toLowerCase().split(",");
    climates.forEach((c) => uniqueClimates.add(c.trim()));
  });

  const climateArray = Array.from(uniqueClimates).sort();

  const allOption = document.createElement("option");
  allOption.value = "all";
  allOption.textContent = "All klimates";
  climateFilter.appendChild(allOption);

  climateArray.forEach((climate) => {
    const option = document.createElement("option");
    option.value = climate;
    option.textContent = climate.charAt(0).toUpperCase() + climate.slice(1);
    climateFilter.appendChild(option);
  });
}

function renderPlanets(filter = "all") {
  planetList.innerHTML = "";

  const filteredPlanets =
    filter === "all"
      ? planets
      : planets.filter((planet) => {
          return planet.climate.toLowerCase().includes(filter);
        });

  filteredPlanets.forEach((planet) => {
    const li = document.createElement("li");
    li.innerHTML = `
          <strong>Name:</strong> ${planet.name}<br />
          <strong>People:</strong> ${planet.population}<br />
          <strong>Klimate:</strong> ${planet.climate}<br />
          <strong>Terrain:</strong> ${planet.terrain}
        `;
    planetList.appendChild(li);
  });
}

(async function init() {
  await fetchPlanets();
  populateClimates();
  renderPlanets();
})();

climateFilter.addEventListener("change", (event) => {
  renderPlanets(event.target.value);
});
