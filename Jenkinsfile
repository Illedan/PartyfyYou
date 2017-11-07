node {
	stage 'Checkout'
		checkout scm

	stage 'Build'
		bat ‪"${nuget} restore src/SpotifyListener.sln"		
	
	stage 'Archive'
		archive 'ProjectName/bin/Release/**'

}
