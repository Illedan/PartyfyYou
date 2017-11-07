node {
	stage 'Checkout'
		checkout scm

	stage 'Build'
		bat 'nuget restore src/SpotifyListener.sln'
		bat "\"${tool 'MSBuild'}\" src/SpotifyListener.sln /p:Configuration=Release /p:Platform=\"Any CPU\" /p:ProductVersion=1.0.0.${env.BUILD_NUMBER}"

	stage 'Archive'
		archive 'ProjectName/bin/Release/**'

}
