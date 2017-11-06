node {
  checkout scm

  docker.build(env.JOB_NAME).inside {
    sh 'script/ci'
  }
}
