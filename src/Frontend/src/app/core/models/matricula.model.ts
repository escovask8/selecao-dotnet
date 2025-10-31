export interface Matricula {
  id: string;
  estudanteId: string;
  cursoId: string;
  dataMatricula: string;
  status: string;
  nomeEstudante?: string;
  tituloCurso?: string;
}

export interface RealizarMatriculaRequest {
  estudanteId: string;
  cursoId: string;
}

