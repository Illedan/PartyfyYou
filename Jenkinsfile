node {
    stage 'Build'
    bat "\"{tool 'MSBuild'}\" SpotifyListener.slnm /p:Configuration=Release /p:Platform=\"Any CPU\" /p:ProductVersion=1.0.0.${env.BUILD_NUMBER}"
}
