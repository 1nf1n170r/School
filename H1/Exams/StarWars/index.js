function createStars() {
  const starsCount = 400;
  for (let i = 0; i < starsCount; i++) {
    let star = document.createElement("div");
    star.className = "stars";
    const [randomX, randomY] = randomPosition();
    star.style.top = randomX + "px";
    star.style.left = randomY + "px";
    document.getElementById("star-wars-intro").append(star);
  }
}

function randomPosition() {
  const width = window.innerWidth;
  const height = window.innerHeight;
  const randomX = Math.floor(Math.random() * height);
  const randomY = Math.floor(Math.random() * width);
  return [randomX, randomY];
}

const startBtn = document.getElementById("startBtn");
const starWarsIntro = document.getElementById("star-wars-intro");
const audio = document.getElementById("star-wars-theme");

startBtn.addEventListener("click", () => {
  startBtn.style.display = "none";
  starWarsIntro.style.display = "block";
  createStars();
  setTimeout(() => {
    audio.play().catch((err) => console.log("Audio playback failed:", err));
  }, 7990);
  setTimeout(() => {
    window.location.href = "./routes/viewer.html";
  }, 100000)
});
