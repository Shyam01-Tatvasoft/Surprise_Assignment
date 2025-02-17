let quizQuestions = [
  {
    question: "What is 10 + 8?",
    options: ["12", "14", "16", "18"],
    answer: "18",
  },
  {
    question: "What is 10 - 9?",
    options: ["1", "13", "11", "15"],
    answer: "1",
  },
  {
    question: "What is 8 x 3?",
    options: ["21", "24", "25", "27"],
    answer: "24",
  },
  {
    question: "What is 12 / 2?",
    options: ["10", "2", "4", "6"],
    answer: "6",
  },
];

let questionContainer = document.getElementById("question");
let optionsContainer = document.getElementById("options");
let next = document.getElementById("next");
let startQuiz = document.getElementById("startQuiz");
let finalScore = document.getElementById("score");
let result = document.getElementById("result")
let main = document.getElementById("que-div");

let questionIndex = 0;
let score = 0;


startQuiz.addEventListener("click", showQuestion);
main.style.display = "none";

function showQuestion() {
  main.style.display = "block"
  startQuiz.style.display = "none";
  next.style.display = "none";
  const question = quizQuestions[questionIndex];
  questionContainer.innerHTML = question.question;

  options.innerHTML = "";
  result.innerHTML = "";
  question.options.forEach((option) => {
    const button = document.createElement("input");
    const label = document.createElement("label");
    const divEle = document.createElement("div");
    button.type = "radio";
    button.name = "option";
    button.value = option;
    label.innerHTML = option;
    divEle.appendChild(button);
    divEle.appendChild(label);
    optionsContainer.appendChild(divEle)
    button.addEventListener("click", selectAnswer);
  });
}

function selectAnswer(e) {
  next.style.display = "block";
  const selectedButton = e.target;
  const correctAnswer = quizQuestions[questionIndex].answer;

  if (selectedButton.value === correctAnswer) {
    score++;
    selectedButton.style.accentColor = "green";
    let arr = optionsContainer.querySelectorAll(`label`);
    arr.forEach((label) => {
      if (label.innerHTML === correctAnswer) {
        label.style.color = "green";
        label.style.fontWeight = "bold";
      }
    });
    result.innerText = "Correct Answer ✅";
    result.style.color = "green"
  } else {
    let arr = optionsContainer.querySelectorAll(`label`);
    arr.forEach((label) => {
      if (label.innerHTML === correctAnswer) {
        label.style.color = "green";
        label.style.fontWeight = "bold";
      }
    });
    result.innerText = "InCorrect Answer ❌";
    result.style.color = "red"
  }
  questionIndex++;
  next.addEventListener("click", showNextQuestion);
}

function showNextQuestion() {
  if (questionIndex < quizQuestions.length) {
    showQuestion();
    next.style.display = "none";
  } else {
    main.style.display = "none";
    finalScore.innerHTML = `Your score is ${score}`;
  }
}
