node {
	stage 'Checkout'
		checkout scm
	
	stage 'Restore'
		bat "\"${tool 'MSBuild'}\" src/SpotifyListener.sln /p:Configuration=Release /p:Platform=\"Any CPU\" /p:ProductVersion=1.0.0.${env.BUILD_NUMBER}"
	
	stage 'Build'	
		bat ‪"${nuget} restore src/SpotifyListener.sln"		
	
	
	stage 'Archive'
		archive 'ProjectName/bin/Release/**'

}
