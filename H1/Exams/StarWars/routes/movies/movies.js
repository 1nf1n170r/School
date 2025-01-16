const filmList = document.getElementById('filmList');
    const crawlModal = document.getElementById('crawlModal');
    const modalCloseBtn = document.getElementById('modalCloseBtn');
    const modalTitle = document.getElementById('modalTitle');
    const modalOpeningCrawl = document.getElementById('modalOpeningCrawl');

    // Fetch film data from SWAPI
    async function fetchFilms() {
      try {
        const response = await fetch('https://swapi.py4e.com/api/films/');
        const data = await response.json();
        return data.results;
      } catch (error) {
        console.error('Fejl under hentning af film:', error);
        return [];
      }
    }

    function renderFilms(films) {
      filmList.innerHTML = '';

      films.forEach(film => {
        const li = document.createElement('li');
        li.innerHTML = `
          <strong>${film.title}</strong><br />
          Release date: ${film.release_date}
        `;

        li.addEventListener('click', () => {
          modalTitle.textContent = film.title;
          modalOpeningCrawl.textContent = film.opening_crawl;
          openModal();
        });
        
        filmList.appendChild(li);
      });
    }

    function openModal() {
      crawlModal.style.display = 'block';
    }

    function closeModal() {
      crawlModal.style.display = 'none';
    }

    modalCloseBtn.addEventListener('click', closeModal);

    window.addEventListener('click', (event) => {
      if (event.target === crawlModal) {
        closeModal();
      }
    });

    (async function init() {
      const films = await fetchFilms();
      renderFilms(films);
    })();
