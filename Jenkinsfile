
pipeline {
    agent any
    stages {
        stage('Build') {
            steps {
                git branch: 'bug/mal-CORS', credentialsId: '797c6067-8265-44d6-a53b-88b72f595efc', url: 'https://github.com/Illedan/PartyfyYou.git'
            }
        }
        stage('Test') {
            steps {
                echo 'Testing..'
            }
        }
        stage('Deploy') {
            steps {
                echo 'Deploying....'
            }
        }
    }
}
