"use client";
import { useState, useEffect } from "react";
import Link from "next/link";
import Image from "next/image";
import styles from "./Header.module.css";

export default function Header() {
  const [mobileMenuOpen, setMobileMenuOpen] = useState(false);
  const [mounted, setMounted] = useState(false);

  useEffect(() => {
    setMounted(true);
  }, []);

  const toggleMobileMenu = () => {
    setMobileMenuOpen(!mobileMenuOpen);
  };

  if (!mounted) {
    return null;
  }

  return (
    <header className={styles.header}>
      <nav className={styles.navbar}>
        <div className={styles["nav-container"]}>
          <div className={styles["nav-left"]}>
            <Link href="/aboutus" className={styles["nav-link"]}>
              About Us
            </Link>
            <Link href="/services" className={styles["nav-link"]}>
              Services
            </Link>
          </div>

          <div className={styles["logo-container"]}>
            <div className={styles["logo-circle"]}>
              <Link href="/">
                <Image
                  src="/img/logo.png"
                  alt="FitOn Logo"
                  width={60}
                  height={60}
                  className={styles["nav-logo"]}
                />
              </Link>
            </div>
          </div>

          <div className={styles["nav-right"]}>
            <Link href="/catalog" className={styles["nav-link"]}>
              Catalog
            </Link>
            <Link href="/contact" className={styles["nav-link"]}>
              Contact
            </Link>
            <Link href="/login" className={styles["login-button"]}>
              Login
            </Link>
          </div>

          <button
            className={styles["menu-toggle"]}
            onClick={toggleMobileMenu}
            aria-label="Toggle menu"
          >
            <div
              className={`${styles.hamburger} ${
                mobileMenuOpen ? styles.active : ""
              }`}
            >
              <span></span>
              <span></span>
              <span></span>
            </div>
          </button>
        </div>
      </nav>

      {/* Mobile Menu */}
      <div
        className={`${styles["mobile-menu"]} ${
          mobileMenuOpen ? styles.active : ""
        }`}
      >
        <div className={styles["mobile-logo-container"]}>
          <Link href="/">
            <Image
              src="/logo-white.png"
              alt="FitOn Logo"
              width={60}
              height={60}
            />
          </Link>
        </div>

        <button
          className={styles["close-menu"]}
          onClick={toggleMobileMenu}
          aria-label="Close menu"
        >
          <span></span>
          <span></span>
        </button>

        <div className={styles["mobile-links"]}>
          <Link
            href="/aboutus"
            className={styles["mobile-link"]}
            onClick={toggleMobileMenu}
          >
            About Us
          </Link>
          <Link
            href="/services"
            className={styles["mobile-link"]}
            onClick={toggleMobileMenu}
          >
            Services
          </Link>
          <Link
            href="/catalog"
            className={styles["mobile-link"]}
            onClick={toggleMobileMenu}
          >
            Catalog
          </Link>
          <Link
            href="/contact"
            className={styles["mobile-link"]}
            onClick={toggleMobileMenu}
          >
            Contact
          </Link>
          <Link
            href="/login"
            className={styles["mobile-link"]}
            onClick={toggleMobileMenu}
          >
            Login
          </Link>
        </div>
      </div>
    </header>
  );
}
