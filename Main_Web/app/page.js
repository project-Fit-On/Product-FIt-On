import styles from "./page.module.css";
import Link from "next/link";
import Image from "next/image";
import Header from "./Pages/Header";
import HowItWorks from "./Pages/HowItWorks";
import WhyChooseFitOn from "./Pages/Feature";
import Contact from "./Pages/Contact";
import Footer from "./Pages/Footer";

export default function Home() {
  return (
    <main className={styles.container}>
      <Header />
      <section className={styles.bannerSection}>
        <div className={styles.bannerContent}>
          <h1 className={styles.title}>Try Before You Buy</h1>
          <h2 className={styles.subtitle}>The Smart Way to Shop</h2>
          <p className={styles.description}>
            Personalized Fashion at Your Fingertips
          </p>

          <div className={styles.buttonContainer}>
            <Link href="/catalog" className={styles.primaryButton}>
              Shop Now
            </Link>
            <Link href="/how-it-works" className={styles.secondaryButton}>
              Learn More
            </Link>
          </div>
        </div>

        <div className={styles.bannerImage}>{/* Banner image */}</div>
      </section>

      <WhyChooseFitOn />
      <HowItWorks />
      <Contact />
      <Footer />
    </main>
  );
}
