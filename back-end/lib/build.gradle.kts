plugins {
    kotlin("multiplatform") version "2.2.21"
    id("it.unibo.collektive.collektive-plugin") version "27.4.0"
}

repositories {
    mavenCentral()
}

kotlin {
    linuxX64("native") {
        binaries {
            sharedLib {
                baseName = "backend"
            }
        }
    }

    sourceSets {
        val commonMain by getting {
            dependencies {
                implementation("it.unibo.collektive:collektive-dsl:27.3.2")
                implementation("it.unibo.collektive:collektive-stdlib:27.4.0")
            }
        }
        val nativeMain by getting
    }
}
