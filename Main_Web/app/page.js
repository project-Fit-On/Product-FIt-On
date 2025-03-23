import styles from "./page.module.css";
import Link from "next/link";
import Image from "next/image";
import Header from "./Components/Header";
import HowItWorks from "./Components/HowItWorks";
import WhyChooseFitOn from "./Components/Feature";
import Contact from "./Components/Contact";
import Footer from "./Components/Footer";

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
            <Link href="#" className={styles.primaryButton}>
              Download IOS App
            </Link>
            <Link href="/contactus" className={styles.secondaryButton}>
              Contact Us
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
