import { assessmentTopics, courseQuestions, onlineTopics } from "./courseLearningContent";

export function CourseLearningContent() {
  return <div className="course-editorial">
    <h3>Aulas online</h3>
    <p>As aulas são organizadas em módulos temáticos e disponibilizadas em formato multimídia, combinando:</p>
    <ul>{onlineTopics.map(text => <li key={text}>{text}</li>)}</ul>
    <p>Cada aula é acompanhada de recursos de acessibilidade, como legendas, transcrição de áudio e navegação intuitiva, garantindo inclusão e autonomia para todos os perfis de usuários.</p>
    <h3>Provas e Avaliações</h3>
    <p>As avaliações são realizadas diretamente na plataforma, com foco na verificação do aprendizado e na aplicação prática dos conhecimentos adquiridos:</p>
    <ul>{assessmentTopics.map(text => <li key={text}>{text}</li>)}</ul>
    <p>Ao concluir o curso e ser aprovado nas avaliações, o aluno recebe um certificado digital reconhecido, que pode ser compartilhado em redes profissionais e utilizado para comprovação de capacitação técnica.</p>
  </div>;
}

export function CourseQuestions({ online }: { online: boolean }) {
  return <div className="course-editorial">
    {courseQuestions(online).map((item, i) => <section className="course-question" key={item.question}>
      <h3>{i + 1}. {item.question}</h3>
      <p>{item.answer}</p>
    </section>)}
  </div>;
}
